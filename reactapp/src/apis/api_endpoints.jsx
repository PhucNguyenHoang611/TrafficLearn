// Login
export const LOGIN = "/login";
export const getLoginBody = (email, password) => ({
  Email: email,
  Password: password,
});

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
export const VERIFY_EMAIL = "/verifyEmail";
export const getVerifyEmailBody = (email, TOTP) => ({
  Email: email,
  TOTP: TOTP,
});
