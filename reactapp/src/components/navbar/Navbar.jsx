import React, { useState } from "react";
import { NavLink, Link, useNavigate } from "react-router-dom";
import NavFine from "./NavFine";
import NavProfile from "./NavProfile";

const NavBar = () => {
  const navigate = useNavigate();
  const [isMenuOpen, setIsMenuOpen] = useState(false);

  function toggleMenu() {
    setIsMenuOpen(!isMenuOpen);
  }

  return (
    <nav className="shadow-md bg-white">
      <section className="lg:max-w-8xl mx-auto px-2 sm:px-6 lg:px-8">
        <div className="relative flex items-center justify-between h-16">
          <div className="absolute inset-y-0 left-0 flex items-center sm:hidden">
            {/* Mobile menu button */}
            <button
              type="button"
              className="inline-flex items-center justify-center p-2 rounded-md text-gray-400 hover:text-white hover:bg-gray-700 focus:outline-none focus:ring-2 focus:ring-inset focus:ring-white"
              aria-controls="mobile-menu"
              aria-expanded="false"
              onClick={toggleMenu}
            >
              <span className="sr-only">Menu</span>
              {/* Icon when menu is closed. */}
              {/* Menu open: "hidden", Menu closed: "block" */}
              <svg
                className="block h-6 w-6"
                xmlns="http://www.w3.org/2000/svg"
                fill="none"
                viewBox="0 0 24 24"
                stroke="currentColor"
                aria-hidden="true"
              >
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  strokeWidth={2}
                  d="M4 6h16M4 12h16M4 18h16"
                />
              </svg>
              {/* Icon when menu is open. */}
              {/* Menu open: "block", Menu closed: "hidden" */}
              <svg
                className="hidden h-6 w-6"
                xmlns="http://www.w3.org/2000/svg"
                fill="none"
                viewBox="0 0 24 24"
                stroke="currentColor"
                aria-hidden="true"
              >
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  strokeWidth={2}
                  d="M6 18L18 6M6 6l12 12"
                />
              </svg>
            </button>
          </div>
          <div className="flex-1 flex items-center justify-center sm:items-stretch sm:justify-start">
            <div onClick={() => navigate("/")} className="flex-shrink-0 flex items-center cursor-pointer">
              <img
                className="hidden lg:block h-10 w-auto"
                src="/logo.jpg"
                alt="Workflow"
              />
              <img
                className="hidden lg:block h-8 w-auto"
                src="/logo_text.png"
                alt="Workflow"
              />
              {/* <p className="uppercase max-sm:hidden mx-2">
                Traffic Learn
              </p> */}
            </div>
            {/* sm:ml-6 -> sm:ml-[12rem] */}
            <div className="hidden sm:block sm:ml-[12rem]">
              <div className="flex space-x-4">
                {/* Current: "bg-gray-900 text-white", Default: "text-gray-300 hover:bg-gray-700 hover:text-white" */}
                <NavFine />

                <Link
                  to={"sign"}
                  className="no-underline text-gray-600 hover:bg-gray-700 hover:text-white px-3 py-2 rounded-md text-sm font-medium"
                >
                  Biển báo giao thông
                </Link>

                <Link
                  to={"review"}
                  className="no-underline text-gray-600 hover:bg-gray-700 hover:text-white px-3 py-2 rounded-md text-sm font-medium"
                >
                  Ôn thi GPLX
                </Link>

                <Link
                  to={"examination"}
                  className="no-underline text-gray-600 hover:bg-gray-700 hover:text-white px-3 py-2 rounded-md text-sm font-medium"
                >
                  Thi thử
                </Link>
                
                <Link
                  to={"news"}
                  className="no-underline text-gray-600 hover:bg-gray-700 hover:text-white px-3 py-2 rounded-md text-sm font-medium"
                >
                  Tin tức giao thông
                </Link>
              </div>
            </div>
          </div>
          <div className="absolute inset-y-0 right-0 flex items-center pr-2 sm:static sm:inset-auto sm:ml-6 sm:pr-0">
            {/* Profile dropdown */}
            <NavProfile />
          </div>
        </div>
      </section>

      {/* Mobile menu, show/hide based on menu state. */}
      {isMenuOpen && (
        <section className="sm:hidden" id="mobile-menu">
          <div className="px-2 pt-2 pb-3 space-y-1">
            {/* Current: "bg-gray-900 text-white", Default: "text-gray-300 hover:bg-gray-700 hover:text-white" */}
            <NavLink
              to={"/fine/motorbike"}
              className={({ isActive }) =>
                isActive
                ? "bg-gray-900 text-white block px-3 py-2 rounded-md text-base font-medium"
                : "text-gray-300 hover:bg-gray-700 hover:text-white block px-3 py-2 rounded-md text-base font-medium"
              }
              onClick={() => setIsMenuOpen(false)}
            >
              Mức phạt giao thông
            </NavLink>

            <NavLink
              to={"/sign"}
              className={({ isActive }) =>
                isActive
                ? "bg-gray-900 text-white block px-3 py-2 rounded-md text-base font-medium"
                : "text-gray-300 hover:bg-gray-700 hover:text-white block px-3 py-2 rounded-md text-base font-medium"
              }
              onClick={() => setIsMenuOpen(false)}
            >
              Biển báo giao thông
            </NavLink>

            <NavLink
              to={"/review"}
              className={({ isActive }) =>
                isActive
                ? "bg-gray-900 text-white block px-3 py-2 rounded-md text-base font-medium"
                : "text-gray-300 hover:bg-gray-700 hover:text-white block px-3 py-2 rounded-md text-base font-medium"
              }
              onClick={() => setIsMenuOpen(false)}
            >
              Ôn thi GPLX
            </NavLink>

            <NavLink
              to={"/examination"}
              className={({ isActive }) =>
                isActive
                ? "bg-gray-900 text-white block px-3 py-2 rounded-md text-base font-medium"
                : "text-gray-300 hover:bg-gray-700 hover:text-white block px-3 py-2 rounded-md text-base font-medium"
              }
              onClick={() => setIsMenuOpen(false)}
            >
              Thi thử
            </NavLink>

            <NavLink
              to={"/news"}
              className={({ isActive }) =>
                isActive
                ? "bg-gray-900 text-white block px-3 py-2 rounded-md text-base font-medium"
                : "text-gray-300 hover:bg-gray-700 hover:text-white block px-3 py-2 rounded-md text-base font-medium"
              }
              onClick={() => setIsMenuOpen(false)}
            >
              Tin tức giao thông
            </NavLink>
          </div>
        </section>
      )}
    </nav>
  );
};

export default NavBar;
