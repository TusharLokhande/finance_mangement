// apiClient.ts
import { loginRequest, msalInstance } from "@/config/authConfig";
import { useLoaderStore } from "@/store/loaderStore";
import axios from "axios";

const apiClient = axios.create({
  baseURL: "/api",
  timeout: 10000,
  headers: {
    "Content-Type": "application/json",
  },
});

let requestCount = 0;

apiClient.interceptors.request.use(
  async (config) => {
    requestCount++;
    useLoaderStore.getState().show();
    const accounts = msalInstance.getAllAccounts();

    if (accounts.length > 0) {
      try {
        const tokenResponse = await msalInstance.acquireTokenSilent({
          ...loginRequest,
          account: accounts[0],
        });
        config.headers.Authorization = `Bearer ${tokenResponse.accessToken}`;
      } catch (err) {
        console.error("Token acquisition failed", err);
        try {
          const tokenResponse =
            await msalInstance.acquireTokenPopup(loginRequest);
          config.headers.Authorization = `Bearer ${tokenResponse.accessToken}`;
        } catch (popupErr) {
          console.error("Popup token acquisition failed", popupErr);
          return Promise.reject(popupErr);
        }
      }
    }
    return config;
  },
  (error) => Promise.reject(error),
);
const handleRequestEnd = () => {
  requestCount--;

  if (requestCount <= 0) {
    requestCount = 0;
    useLoaderStore.getState().hide();
  }
};

apiClient.interceptors.response.use(
  (response) => {
    handleRequestEnd();
    return response.data;
  },
  async (error) => {
    handleRequestEnd();
    if (error.response) {
      console.error("API Error:", error.response.data);
      if (error.response.status === 401) {
        const accounts = msalInstance.getAllAccounts();
        if (accounts.length > 0) {
          try {
            console.warn(
              "Unauthorized - attempting token refresh via popup...",
            );
            const tokenResponse =
              await msalInstance.acquireTokenPopup(loginRequest);
            error.config.headers.Authorization = `Bearer ${tokenResponse.accessToken}`;
            return apiClient(error.config);
          } catch (popupErr) {
            console.error("Token refresh failed", popupErr);
          }
        }
      }
    } else {
      console.error("Network error:", error.message);
    }
    return Promise.reject(error);
  },
);

export default apiClient;
