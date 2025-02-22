import { login, persist } from "@/lib/api";
import { useDispatch } from "react-redux";
import { setToken, clearToken } from "@/store/userSlice";
import { useRouter } from "next/navigation";

export function useAuth() {
  const dispatch = useDispatch();
  const router = useRouter();

  async function loginUser(username: string, password: string) {
    const userData = await login(username, password);
    console.log(userData)
    console.log(userData.token)
    if (userData.success) {
        localStorage.setItem("@kanboom:token", userData.token);
        dispatch(setToken(userData.token));
        // router.push("/user");
    }
  }

  async function persistUser(token: string){
    const userData = await persist(token);
    if (userData.success) {
        localStorage.setItem("@kanboom:token", userData.token);
        dispatch(setToken(userData.token));
    }
  }

  function logoutUser() {
    localStorage.removeItem("@kanboom:token");
    dispatch(clearToken());
    router.push("/login");
  }

  return { loginUser, logoutUser, persistUser };
}