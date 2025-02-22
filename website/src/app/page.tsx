'use client';
import { useEffect } from 'react';
import { useRouter } from 'next/navigation';
// import './styles/main.scss';

export default function HomePage() {
  
  const router = useRouter();

  useEffect(() => {
    if (localStorage.getItem('@kanboom:token')) {
      router.push('/user');
    }
  })

  return (
    <div className="home-wrapper">
       kanboom
    </div>
  );
}