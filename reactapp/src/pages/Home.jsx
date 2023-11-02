import React, { useEffect, useState } from "react";
import { Outlet } from "react-router-dom";
import NavBar from "@/components/navbar/NavBar";
import Footer from "@/components/footer/Footer";

const Home = () => {
  const [visible, setVisible] = useState(true);
  const [prevScrollPos, setPrevScrollPos] = useState(0);

  // Scroll
  useEffect(() => {
    const handleScroll = () => {
      const currentScrollPos = window.pageYOffset;
      setVisible(currentScrollPos < 10 || prevScrollPos > currentScrollPos);
      setPrevScrollPos(currentScrollPos);
    };

    window.addEventListener("scroll", handleScroll);

    return () => window.removeEventListener("scroll", handleScroll);
  }, [prevScrollPos, visible]);

  return (
    <>
      <header
        className={`fixed top-0 z-50 w-full transition-all duration-300 
      ${visible ? "visible" : "invisible"}
      ${visible ? "opacity-100" : "opacity-0"}
      `}
      >
        <NavBar />
      </header>
      {/* main */}
      <main className="mt-[80px] max-w-8xl mx-auto px-2 sm:px-6 lg:px-8">
        <Outlet />
      </main>
      <footer>
        <Footer />
      </footer>
    </>
  );
};

export default Home;
