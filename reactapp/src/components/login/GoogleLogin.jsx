import { useNavigate } from "react-router-dom";
import { useDispatch } from "react-redux";
import { googleLoginCallback } from "../../apis/api_function";

const GoogleLogin = () => {
  const navigate = useNavigate();
  const dispatch = useDispatch();

  const handleLoginWithGoogle = async () => {
    try {
      dispatch({ type: "GG" });
      googleLoginCallback();
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <>
      <button
        className="relative mt-6 border rounded-md py-2 text-sm text-gray-800 bg-gray-100 hover:bg-gray-200"
        onClick={handleLoginWithGoogle}
      >
        <span className="absolute left-16 top-0 flex items-center justify-center h-full w-10 text-blue-500">
          <svg
            xmlns="http://www.w3.org/2000/svg"
            height="1em"
            viewBox="0 0 488 512"
          >
            <path d="M488 261.8C488 403.3 391.1 504 248 504 110.8 504 0 393.2 0 256S110.8 8 248 8c66.8 0 123 24.5 166.3 64.9l-67.5 64.9C258.5 52.6 94.3 116.6 94.3 256c0 86.5 69.1 156.6 153.7 156.6 98.2 0 135-70.4 140.8-106.9H248v-85.3h236.1c2.3 12.7 3.9 24.9 3.9 41.4z" />
          </svg>
        </span>
        <span>Đăng nhập với Google</span>
      </button>
    </>
  );
};

export default GoogleLogin;
