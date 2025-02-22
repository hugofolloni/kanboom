// app/login/page.tsx
"use client";

import { useState } from "react";
import { useAuth } from "@/hooks/useAuth";

// LoginPage component
export default function LoginPage() {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const { loginUser } = useAuth();
 
  return (
    <div className="login-container">
        <h1>login</h1>
        <input type="text" placeholder="username" value={username} onChange={(e) => setUsername(e.target.value)} />
        <input type="password" placeholder="password" value={password} onChange={(e) => setPassword(e.target.value)} />
        <button onClick={() => loginUser(username, password)}> Login </button>
    </div>
  );
}
