import { create } from "zustand";

const STORAGE_KEY = "auth_session";

type UserType = {
  id?: string;
  name?: string;
  email?: string;
};

type AuthState = {
  user: UserType | null;
  accessToken: string | null;
  setSession: (user: UserType, token: string) => void;
  clearSession: () => void;
  restoreSession: () => void;
};

export const useAuthStore = create<AuthState>((set, get) => ({
  user: null,
  accessToken: null,

  setSession: (user: UserType, token: string) => {
    const session = { user, accessToken: token };
    sessionStorage.setItem(STORAGE_KEY, JSON.stringify(session));

    set({
      user,
      accessToken: token,
    });
  },

  clearSession: () => {
    sessionStorage.removeItem(STORAGE_KEY);

    set({
      user: null,
      accessToken: null,
    });
  },

  restoreSession: () => {
    const stored = sessionStorage.getItem(STORAGE_KEY);
    if (!stored) return;

    const session = JSON.parse(stored);

    set({
      user: session.user,
      accessToken: session.accessToken,
    });
  },
}));
