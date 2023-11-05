import { Button } from "@mui/material";
import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { theme } from "@/themes/CustomeTheme";
import { useSelector } from "react-redux";
import car from "/public/car.jpg";
import crossRoad from "/public/crossRoad.jpg";

const Landing = () => {
  const auth = useSelector((state) => state.auth);
  const [isLogin, setIsLogin] = useState(false);
  useEffect(() => {
    if (auth.token !== "" && auth.token !== null) {
      setIsLogin(true);
    }
  }, [auth.token]);
  return (
    <div className="">
      <div className="flex justify-center items-center">
        <div className="flex flex-col justify-center items-center text-center">
          <h1 className="text-4xl font-bold mb-8 text-gray-800">
            Chào mừng bạn đến với website học luật giao thông online
          </h1>
          <p className="text-lg mb-8 text-gray-600">
            Tìm hiểu về luật giao thông đường bộ, biển báo giao thông, mức phạt,
            các tin tức giao thông mới nhất cũng như ôn tập cho kỳ thi giấy phép
            lái xe.
          </p>

          {isLogin ? (
            <Button
              variant="contained"
              component={Link}
              sx={{
                backgroundColor: theme.palette.secondary.main,
                ":hover": { backgroundColor: theme.palette.secondary.dark },
              }}
            >
              <span>Thi thử ngay bây giờ</span>
            </Button>
          ) : (
            <Button
              variant="contained"
              component={Link}
              to="/signup"
              sx={{
                backgroundColor: theme.palette.secondary.main,
                ":hover": { backgroundColor: theme.palette.secondary.dark },
              }}
            >
              <span>Đăng ký ngay để bắt đầu</span>
            </Button>
          )}
        </div>
        <div className="max-w-[36rem]">
          <img src={crossRoad} alt="Cross Road Image" />
        </div>
      </div>

      <div className="flex items-center justify-center w-full">
        <div className="max-w-[50rem] min-w-[20rem]">
          <img
            src={car}
            alt="Traffic Landing Image"
            className="h-auto w-full max-w-screen-lg"
          />
        </div>
        <div>
          <h2 className="font-bold text-lg">
            Định làm j đó nhưng chưa nghĩ ra sẽ nhét j
          </h2>
          <p>
            lorem ipsum dolor sit amet consectetur adipisicing elit. Quisquam
          </p>
        </div>
      </div>
    </div>
  );
};

export default Landing;
