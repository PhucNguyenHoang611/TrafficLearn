import { useEffect } from "react";
import {
  createBrowserRouter,
  createRoutesFromElements,
  Route,
  RouterProvider,
  useNavigate,
} from "react-router-dom";
import RootPage from "../pages/RootPage";
import NotFound from "../pages/NotFound";
import Home from "../pages/Home";
import TrafficFine from "../pages/TrafficFine";

export const router = createBrowserRouter(
  createRoutesFromElements(
    <Route path="/" element={<RootPage />}>
      <Route index element={<Home />} />
      <Route path="/" element={<Home />}>
        <Route path="fine/:fineId" element={<TrafficFine />}></Route>
      </Route>

      <Route path="*" element={<NotFound />} />
    </Route>
  )
);
