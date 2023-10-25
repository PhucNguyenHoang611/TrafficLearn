import React from "react";

const Footer = () => {
  return (
    <footer className="bg-den">
      <div className="max-w-7xl mx-auto py-12 px-4 sm:px-6 lg:py-16 lg:px-8">
        <div className="xl:grid xl:grid-cols-3 xl:gap-8">
          <div className="grid grid-cols-1 gap-8 xl:col-span-2 md:grid-cols-2">
            <div>
              <h3 className="text-lg font-medium leading-6 text-gray-200">
                About Us
              </h3>
              <p className="mt-2 max-w-md text-sm leading-5 text-gray-400">
                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed
                euismod, diam id blandit elementum, eros est lacinia lacus, vel
                tincidunt mi nibh sit amet lorem.
              </p>
              <div className="mt-8">
                <h3 className="text-lg font-medium leading-6 text-gray-200">
                  Contact Us
                </h3>
                <div className="mt-2 max-w-md text-sm leading-5 text-gray-400">
                  <p>1234 Main St.</p>
                  <p>Anytown, USA 12345</p>
                  <p>info@example.com</p>
                  <p>555-555-5555</p>
                </div>
              </div>
            </div>
            <div className="mt-12 md:mt-0">
              <h3 className="text-lg font-medium leading-6 text-gray-200">
                Follow Us
              </h3>
              <div className="mt-2 flex">
                <a
                  href="#"
                  className="text-gray-400 hover:text-white transition duration-150 ease-in-out"
                >
                  <span className="sr-only">Facebook</span>
                  <svg
                    className="h-6 w-6"
                    fill="currentColor"
                    viewBox="0 0 24 24"
                  >
                    <path
                      fillRule="evenodd"
                      d="M21 3H3a2 2 0 00-2 2v16a2 2 0 002 2h9.31v-7.37h-2.54v-2.86h2.54V9.53c0-2.52 1.54-3.89 3.77-3.89 1.1 0 2.05.08 2.32.12v2.6l-1.59.001c-1.25 0-1.49.59-1.49 1.46v1.92h2.98l-.39 2.86h-2.59V21H21a2 2 0 002-2V5a2 2 0 00-2-2z"
                      clipRule="evenodd"
                    />
                  </svg>
                </a>
                <a
                  href="#"
                  className="ml-6 text-gray-400 hover:text-white transition duration-150 ease-in-out"
                >
                  <span className="sr-only">Twitter</span>
                  <svg
                    className="h-6 w-6"
                    fill="currentColor"
                    viewBox="0 0 24 24"
                  >
                    <path
                      fillRule="evenodd"
                      d="M23.998 4.575a9.13 9.13 0 01-2.6.712 4.57 4.57 0 002.005-2.527 9.14 9.14 0 01-2.893 1.106 4.556 4.556 0 00-7.81 4.145 12.95 12.95 0 01-9.4-4.77 4.557 4.557 0 001.41 6.077 4.51 4.51 0 01-2.07-.57v.06a4.558 4.558 0 003.648 4.47 4.52 4.52 0 01-2.064.078 4.558 4.558 0 004.25 3.15 9.132 9.132 0 01-5.662 1.95c-.368 0-.73-.022-1.09-.067a12.92 12.92 0 007.008 2.05c8.4 0 12.997-6.96 12.997-12.997l-.014-.59A9.305 9.305 0 0024 3.542a9.15 9.15 0 01-2.002.033z"
                      clipRule="evenodd"
                    />
                  </svg>
                </a>
                <a
                  href="#"
                  className="ml-6 text-gray-400 hover:text-white transition duration-150 ease-in-out"
                >
                  <span className="sr-only">Instagram</span>
                  <svg
                    className="h-6 w-6"
                    fill="currentColor"
                    viewBox="0 0 24 24"
                  >
                    <path
                      fillRule="evenodd"
                      d="M12 2.25a9.75 9.75 0 109.75 9.75A9.76 9.76 0 0012 2.25zm5.5 11.5a5.5 5.5 0 11-11 0 5.5 5.5 0 0111 0zm-3.3-4.4a2.2 2.2 0 10-4.4 0 2.2 2.2 0 004.4 0zm3.3-.3a3 3 0 11-6 0 3 3 0 016 0z"
                      clipRule="evenodd"
                    />
                  </svg>
                </a>
              </div>
            </div>
          </div>
        </div>
        <div className="mt-12 border-t border-gray-700 pt-8">
          <p className="text-base leading-6 text-gray-400 xl:text-center">
            &copy; 2023 Traffic Learning. All rights reserved.
          </p>
        </div>
      </div>
    </footer>
  );
};

export default Footer;
