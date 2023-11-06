import { Button } from "@mui/material";
import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { theme } from "@/themes/CustomeTheme";
import { useSelector } from "react-redux";
import crossRoad from "/crossRoad.svg";
import "react-lazy-load-image-component/src/effects/blur.css";
import LandFooter from "../components/landing/LandFooter";
import LandRedirect from "../components/landing/LandRedirect";

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
      <div className="flex max-sm:flex-col ms:justify-center items-center">
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
        <div>
          {/* <LazyLoadImage src={crossRoad} alt="Cross Road Image" effect="blur" /> */}
          <img
            src={crossRoad}
            alt="Cross Road Image"
            width={600}
            height={600}
          />
        </div>
      </div>
      <LandRedirect />
      <LandFooter />
    </div>
  );
};

export default Landing;
