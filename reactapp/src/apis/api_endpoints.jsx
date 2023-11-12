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
