import React, { useState, useRef, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Link, useNavigate } from "react-router-dom";

const NavProfile = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const auth = useSelector((state) => state.auth);
  const [isOpen, setIsOpen] = useState(false);
  const [isLogout, setIsLogout] = useState(false);
  const dropdownRef = useRef(null);

  const handleLogout = () => {
    navigate("/login");

    localStorage.removeItem("auth");
    localStorage.removeItem("timeRemaining");
    localStorage.removeItem("examId");

    dispatch({ type: "LOGOUT" });
  };

  const toggleDropdown = () => {
    setIsOpen(!isOpen);
  };

  useEffect(() => {
    // Close dropdown menu if clicked on outside of element
    const handleClickOutside = (event) => {
      if (dropdownRef.current && !dropdownRef.current.contains(event.target)) {
        setIsOpen(false);
      }
    };

    document.addEventListener("click", handleClickOutside);
    return () => {
      document.removeEventListener("click", handleClickOutside);
    };
  }, []);

  useEffect(() => {
    // Check if auth is changed
    if (auth.token === "") {
      setIsLogout(true);
    }
  }, [auth]);

  return (
    <>
      <div className="ml-3 relative" ref={dropdownRef}>
        <div>
          <button
            type="button"
            className="bg-gray-800 flex text-sm rounded-full focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-offset-gray-800 focus:ring-white"
            id="user-menu"
            aria-expanded={isOpen}
            aria-haspopup="true"
            onClick={toggleDropdown}
          >
            <span className="sr-only">Open user menu</span>
            <img
              className="h-8 w-8 rounded-full"
              src="https://cdn-icons-png.flaticon.com/512/6596/6596121.png"
              alt=""
            />
          </button>
        </div>
        {/*
                Profile dropdown panel, show/hide based on dropdown state.

                Entering: "transition ease-out duration-100"
                  From: "transform opacity-0 scale-95"
                  To: "transform opacity-100 scale-100"
                Leaving: "transition ease-in duration-75"
                  From: "transform opacity-100 scale-100"
                  To: "transform opacity-0 scale-95"
              */}
        {isOpen && (
          <div
            className="origin-top-right absolute right-0 mt-2 w-48 rounded-md shadow-lg py-1 bg-white ring-1 ring-black ring-opacity-5"
            role="menu"
            aria-orientation="vertical"
            aria-labelledby="user-menu"
          >
            {isLogout ? (
              <>
                <button
                  href=""
                  className="block w-full px-4 py-2 text-left text-sm bg-transparent border-none text-gray-700 hover:bg-gray-100"
                  role="menuitem"
                  onClick={() => navigate("/login")}
                >
                  Đăng nhập
                </button>
              </>
            ) : (
              <>
                <button
                  href=""
                  className="block w-full px-4 py-2 text-left text-sm bg-transparent border-none text-gray-700 hover:bg-gray-100"
                  role="menuitem"
                >
                  Tài khoản
                </button>

                <Link to={"/history"}>
                  <button
                    href=""
                    className="block w-full px-4 py-2 text-left text-sm bg-transparent border-none text-gray-700 hover:bg-gray-100"
                    role="menuitem"
                  >
                    Lịch sử
                  </button>
                </Link>

                <button
                  className="block w-full px-4 py-2 text-left text-hong text-sm bg-transparent border-none hover:bg-gray-100"
                  role="menuitem"
                  onClick={handleLogout}
                >
                  Đăng xuất
                </button>
              </>
            )}
          </div>
        )}
      </div>
    </>
  );
};

export default NavProfile;
