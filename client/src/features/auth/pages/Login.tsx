import { useAuthStore } from "@/app/store/user-store";
import { Button } from "@/components/ui/button";
import { msalLogin } from "@/config/authConfig";
import { useNavigate } from "react-router-dom";
import { useLogin } from "../hooks/queries/use-login-hook";

const Login = () => {
  const { setSession } = useAuthStore();
  const { refetch } = useLogin();
  const navigate = useNavigate();
  const handleLogin = async () => {
    try {
      const result = await msalLogin();
      await refetch();
      setSession(
        {
          id: result.account?.homeAccountId,
          name: result.name,
          email: result.email,
        },
        result.accessToken,
      );
      navigate("/");
    } catch (err) {
      console.error(err);
    }
  };

  return (
    <div className="flex w-full h-screen ">
      <div className="w-1/2 h-full bg-primary-500"></div>
      <div className="w-1/2 h-full bg-background">
        <div className="flex flex-col items-center justify-center h-full gap-6">
          <h1 className="text-3xl font-bold">Welcome Back</h1>
          <Button onClick={handleLogin}>Sign in with Microsoft</Button>
        </div>
      </div>
    </div>
  );
};

export default Login;
