import { create } from "zustand";
import { createJSONStorage, persist } from "zustand/middleware";

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
};

export const useAuthStore = create<AuthState>()(
  persist(
    (set) => ({
      user: null,
      accessToken: null,

      setSession: (user, token) =>
        set({
          user,
          accessToken: token,
        }),

      clearSession: () =>
        set({
          user: null,
          accessToken: null,
        }),
    }),
    {
      name: "auth_session",
      storage: createJSONStorage(() => sessionStorage),
    },
  ),
);
