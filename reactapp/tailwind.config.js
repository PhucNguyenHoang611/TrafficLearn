/** @type {import('tailwindcss').Config} */
/*eslint-env node*/
module.exports = {
  content: ["./index.html", "./src/**/*.{js,jsx}"],
  theme: {
    extend: {
      transitionProperty: {
        width: "width",
        height: "height",
      },
      colors: {
        cam: "#ff9c72",
        vang: "#fffd84",
        hong: "#f0217d",
        xanh: "#1b9a8c",
        den: "#292c42",
      },
      screens: {
        phone: "444px",
        // => @media (min-width: 640px) { ... }

        "phone-1": "516px",
        // => @media (min-width: 640px) { ... }

        tablet: "640px",
        // => @media (min-width: 640px) { ... }

        laptop: "1024px",
        // => @media (min-width: 1024px) { ... }

        desktop: "1280px",
        // => @media (min-width: 1280px) { ... }
      },
    },
  },
  plugins: [],
  corePlugins: {
    preflight: false,
  },
};
