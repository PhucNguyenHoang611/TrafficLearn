import React, { useState, useRef, useEffect } from "react";
import { Link } from "react-router-dom";

const NavFine = () => {
  const [isOpen, setIsOpen] = useState(false);
  const menuRef = useRef(null);

  const handleMouseEnter = () => {
    setIsOpen(true);
  };

  const handleMouseLeave = (event) => {
    if (
      !event.relatedTarget ||
      (!event.relatedTarget.contains(menuRef.current) &&
        event.relatedTarget !== menuRef.current)
    ) {
      setIsOpen(false);
    }
  };

  const handleMenuMouseEnter = () => {
    setIsOpen(true);
  };

  useEffect(() => {
    const handleClick = (event) => {
      if (
        menuRef.current &&
        !event.target.contains(menuRef.current) &&
        event.target !== menuRef.current.previousSibling
      ) {
        setIsOpen(false);
      }
    };

    document.addEventListener("click", handleClick);

    return () => {
      document.removeEventListener("click", handleClick);
    };
  }, []);

  return (
    <div className="relative">
      <button
        // className="bg-den text-white py-2 px-4 rounded-md text-sm font-medium"
        className="no-underline text-gray-600 hover:bg-gray-700 hover:text-white px-3 py-2 rounded-md text-sm font-medium"
        onMouseEnter={handleMouseEnter}
        onMouseLeave={handleMouseLeave}
      >
        Mức phạt giao thông
      </button>
      {isOpen && (
        <div
          className="absolute z-50 top-9 right-0 mt-2 py-2 w-48 bg-white rounded-md shadow-lg ring-1 ring-black ring-opacity-5"
          onMouseEnter={handleMenuMouseEnter}
          ref={menuRef}
        >
          <Link
            to={"fine/motorbike"}
            className="no-underline block px-4 py-2 text-gray-800 hover:bg-gray-100 hover:text-gray-900"
          >
            Xe máy
          </Link>
          <Link
            to={"fine/car"}
            className="no-underline block px-4 py-2 text-gray-800 hover:bg-gray-100 hover:text-gray-900"
          >
            Xe ô tô
          </Link>
          <Link
            to={"fine/other"}
            className="no-underline block px-4 py-2 text-gray-800 hover:bg-gray-100 hover:text-gray-900"
          >
            Khác
          </Link>
        </div>
      )}
    </div>
  );
};

export default NavFine;
