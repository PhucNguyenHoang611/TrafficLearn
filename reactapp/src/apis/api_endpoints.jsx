export const getAccessToken = (token) => ({
  headers: {
    Authorization: "Bearer " + token,
  },
});

// Login
export const LOGIN = "/login";
export const getLoginBody = (email, password) => ({
  Email: email,
  Password: password,
});

// Login with Google
export const LOGIN_GOOGLE = "/login/google/callback";
export const getLoginGoogleBody = (tokenId) => ({
  TokenId: tokenId,
});

// Login Google success
export const LOGIN_GOOGLE_SUCCESS = "/login/google/success";

// https://localhost:7220/api/login/google/success

// Register
export const REGISTER = "/register";
export const getRegisterBody = (
  email,
  password,
  firstName,
  lastName,
  birthday,
  gender,
  phoneNumber
) => ({
  Email: email,
  Password: password,
  FirstName: firstName,
  LastName: lastName,
  Birthday: birthday,
  Gender: gender,
  PhoneNumber: phoneNumber,
  Provider: "default",
  Role: "User",
});

// Verify Email
export const VERIFY_EMAIL = "/register/verifyEmail";
export const getVerifyEmailBody = (email, TOTP) => ({
  Email: email,
  TOTP: TOTP,
});

// Send verify email
export const SEND_VERIFY_EMAIL = (email) =>
  `/register/sendVerificationEmail/${email}`;
export const getSendVerifyEmailBody = (email) => ({
  Email: email,
});

// Check valid
export const CHECK_VALID = "/register/checkValid";
export const getCheckValidBody = (email) => ({
  Email: email,
});

// Forget password
export const FORGET_PASSWORD = (email) => `/login/forgetPassword/${email}`;
export const getForgetPasswordBody = (email) => ({
  Email: email,
});

// Reset password
export const RESET_PASSWORD = "/login/resetPassword";
export const getResetPasswordBody = (email, password, TOTP) => ({
  Email: email,
  TOTP: TOTP,
  NewPassword: password,
});


// Traffic fine
export const GET_ALL_TRAFFIC_FINES = "/trafficFine/getAllTrafficFines";

export const GET_TRAFFIC_FINE = (id) => `/trafficFine/getTrafficFineById/${id}`;

// Traffic fine type
export const GET_ALL_TRAFFIC_FINE_TYPES = "/trafficFineType/getAllTrafficFineTypes";

export const GET_TRAFFIC_FINE_TYPE = (id) => `/trafficFineType/getTrafficFineTypeById/${id}`;

// Traffic sign
export const GET_ALL_TRAFFIC_SIGNS = "/trafficSign/getAllTrafficSigns";

export const GET_TRAFFIC_SIGN = (id) => `/trafficSign/getTrafficSignById/${id}`;

// Traffic sign type
export const GET_ALL_TRAFFIC_SIGN_TYPES = "/trafficSignType/getAllTrafficSignTypes";

export const GET_TRAFFIC_SIGN_TYPE = (id) => `/trafficSignType/getTrafficSignTypeById/${id}`;

// News
export const GET_ALL_NEWS = "/news/getAllNews";

export const GET_NEWS = (id) => `/news/getNewsById/${id}`;

// License
export const GET_ALL_LICENSES = "/license/getAllLicenses";

export const GET_LICENSE = (id) => `/license/getLicenseById/${id}`;

// Title
export const GET_ALL_TITLES = "/title/getAllTitles";

export const GET_TITLE = (id) => `/title/getTitleById/${id}`;

// License title
export const GET_ALL_LICENSE_TITLES = "/licenseTitle/getAllLicenseTitles";

export const GET_LICENSE_TITLE = (id) => `/licenseTitle/getLicenseTitleById/${id}`;

export const GET_LICENSE_TITLES_BY_LICENSE_ID = (id) => `/licenseTitle/getLicenseTitlesByLicenseId/${id}`;

// Question
export const GET_ALL_QUESTIONS_BY_LICENSE_ID = (id) => `/question/getAllQuestionsByLicenseId/${id}`;

export const GET_ALL_IMPORTANT_QUESTIONS_BY_LICENSE_ID = (id) => `/question/getAllImportantQuestionsByLicenseId/${id}`;

export const GET_QUESTIONS_BY_LICENSE_TITLE_ID = (id) => `$/question/getQuestionsByLicenseTitleId/{id}`;