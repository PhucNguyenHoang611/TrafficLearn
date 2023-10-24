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
        "primary-0": "#423A8E",
        "primary-1": "#00CCCD",
        "secondary-0": "#FFC107",
        "secondary-1": "#DC3545",
        "secondary-2": "#198754",
        "secondary-3": "#0D6EFD",
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
