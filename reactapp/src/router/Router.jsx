import { useEffect, lazy } from "react";
import {
  createBrowserRouter,
  createRoutesFromElements,
  Navigate,
  redirect,
  Route,
  RouterProvider,
  useNavigate,
} from "react-router-dom";
import RootPage from "../pages/RootPage";
import NotFound from "../pages/NotFound";
import Home from "../pages/Home";
import TrafficFine from "../pages/TrafficFine";
import TrafficSign from "../pages/TrafficSign";
import ExamHistory from "@/pages/ExamHistory";
import Login from "../pages/Login";
import SignUp from "../pages/SignUp";
import ForgotPassword from "../pages/ForgotPassword";
import ResetPassword from "../pages/ResetPassword";
import Verify from "../pages/Verify";
import Landing from "../pages/Landing";
import News from "../pages/News";
import NewsList from "../components/news/NewsList";
import NewsDetails from "../components/news/NewsDetails";
import Review from "../pages/Review/Review";
import Examination from "../pages/Review/Examination/Examination";
import ExamPage from "../pages/Review/Examination/ExamPage";
import ExamResult from "../pages/Review/Examination/ExamResult";
import ReviewDetails from "../pages/Review/ReviewDetails";
// const Landing = lazy(() => import("../pages/Landing"));

const authLoader = () => {
  if (!JSON.parse(localStorage.getItem("auth")))
    return redirect("/login");
  else
    return true;
};

export const router = createBrowserRouter(
  createRoutesFromElements(
    <Route path="/" element={<RootPage />}>
      {/* <Route index element={<Home />} /> */}
      <Route path="/" element={<Home />}>
        <Route index element={<Landing />}></Route>
        {/* <Route path="landing" element={<Landing />}></Route> */}
        <Route path="fine/:vehicleType" element={<TrafficFine />}></Route>
        <Route path="sign" element={<TrafficSign />}></Route>
        <Route path="news" element={<News />}>
          <Route index element={<NewsList />} />
          <Route path=":newsId" element={<NewsDetails />} />
        </Route>
        <Route path="review" element={<Review />}></Route>
        <Route path="reviewDetails" element={<ReviewDetails />}></Route>
        <Route path="examination" element={<Examination />}></Route>

        <Route path="exam/:examId" element={<ExamPage />} loader={authLoader}></Route>
        <Route path="result/:examId" element={<ExamResult />} loader={authLoader}></Route>
        <Route path="history" element={<ExamHistory />} loader={authLoader}></Route>
      </Route>
      <Route path="login" element={<Login />} />
      <Route path="signup" element={<SignUp />} />
      <Route path="verify" element={<Verify />} />
      <Route path="forgotpassword" element={<ForgotPassword />} />
      <Route path="resetpassword" element={<ResetPassword />} />

      {(JSON.parse(localStorage.getItem("auth"))) ? (
        <Route path="*" element={<NotFound />} />
      ) : (
        <Route path="*" element={<Navigate to="login" />} />
      )}
    </Route>
  )
);
