// apiClient.ts
import { useLoaderStore } from "@/store/loaderStore";
import axios from "axios";

const apiClient = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL,
  timeout: 10000,
  headers: {
    "Content-Type": "application/json",
  },
});

let requestCount = 0;

// ✅ SINGLE request interceptor
apiClient.interceptors.request.use(
  (config) => {
    requestCount++;

    // show loader
    useLoaderStore.getState().show();

    // attach token
    // const token = localStorage.getItem("token");
    // if (token) {
    //   config.headers.Authorization = `Bearer ${token}`;
    // }

    return config;
  },
  (error) => Promise.reject(error),
);

// ✅ helper to safely hide loader
const handleRequestEnd = () => {
  requestCount--;

  if (requestCount <= 0) {
    requestCount = 0;
    useLoaderStore.getState().hide();
  }
};

// ✅ response interceptor
apiClient.interceptors.response.use(
  (response) => {
    handleRequestEnd();
    return response.data;
  },
  (error) => {
    handleRequestEnd();

    if (error.response) {
      console.error("API Error:", error.response.data);

      if (error.response.status === 401) {
        console.warn("Unauthorized - redirecting...");
      }
    } else {
      console.error("Network error:", error.message);
    }

    return Promise.reject(error);
  },
);

export default apiClient;
