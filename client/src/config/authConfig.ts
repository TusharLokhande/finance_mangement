import { PublicClientApplication } from "@azure/msal-browser";

export const msalConfig = {
  auth: {
    clientId: import.meta.env.VITE_API_CLIENT_ID,
    authority: `https://login.microsoftonline.com/${import.meta.env.VITE_TENANT_ID}`,
    redirectUri: import.meta.env.VITE_REDIRECT_URI,
    postLogoutRedirectUri: "/login",
  },
  cache: {
    cacheLocation: "sessionStorage",
    storeAuthStateInCookie: false,
  },
};

export const loginRequest = {
  scopes: ["api://f40a9b06-bd62-4750-ba3f-15c845c4b357/access_as_user"],
};
export const msalInstance = new PublicClientApplication(msalConfig);

export const msalLogout = async () => {
  const account = msalInstance.getActiveAccount();

  if (account) {
    await msalInstance.logoutRedirect({
      account: account,
      postLogoutRedirectUri: "/login",
    });
  } else {
    await msalInstance.logoutRedirect({
      postLogoutRedirectUri: "/login",
    });
  }
};

export const msalLogin = async () => {
  try {
    const loginResponse = await msalInstance.loginPopup({
      scopes: [...loginRequest.scopes],
      redirectUri: import.meta.env.VITE_REDIRECT_URI,
    });
    return {
      account: loginResponse.account,
      accessToken: loginResponse.accessToken,
      name: loginResponse.account?.name || "",
      email: loginResponse.account?.username || "",
    };
  } catch (err) {
    console.error("Login failed", err);
    throw err;
  }
};
