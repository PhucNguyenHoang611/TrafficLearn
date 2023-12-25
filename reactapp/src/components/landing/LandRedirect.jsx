import { Button } from "@mui/material";
import React from "react";
import { Link } from "react-router-dom";

const LandRedirect = () => {
  return (
    <div className="grid grid-cols-2 md:grid-cols-4 mx-auto my-8">
      {/* 1 */}
      <div className="max-w-sm rounded overflow-hidden pl-1 pb-1 text-center">
        <div className="px-6 py-4">
          <img
            src="https://media.istockphoto.com/id/1169108120/vector/penalty-icon.jpg?s=612x612&w=0&k=20&c=1N3dGBTOopxGtPTfCA2btvhVm15gqlkn2dxSpzr8EFs="
            alt=""
            className="w-24 h-24 mx-auto"
          />
          <div className="font-bold text-xl mb-2">Mức phạt giao thông</div>
          <p className="text-gray-700 text-base">
            Tìm hiểu về các mức phạt giao thông mới nhất
          </p>
        </div>
      </div>
      {/* 2 */}
      <div className="max-w-sm rounded overflow-hidden pl-1 pb-1 text-center">
        <div className="px-6 py-4">
          <img
            src="https://cdn-icons-png.flaticon.com/512/675/675642.png"
            alt=""
            className="w-24 h-24 mx-auto"
          />
          <div className="font-bold text-xl mb-2">Biển báo giao thông</div>
          <p className="text-gray-700 text-base">
            Tìm hiểu về các biển báo giao thông mới nhất
          </p>
        </div>
      </div>
      {/* 3 */}
      <div className="max-w-sm rounded overflow-hidden pl-1 pb-1 text-center">
        <div className="px-6 py-4">
          <img
            src="data:image/png;base64,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"
            alt=""
            className="w-24 h-24 mx-auto"
          />
          <div className="font-bold text-xl mb-2">Ôn thi GPLX</div>
          <p className="text-gray-700 text-base">
            Ôn tập cho kỳ thi giấy phép lái xe
          </p>
        </div>
      </div>
      {/* 4 */}
      <div className="max-w-sm rounded overflow-hidden pl-1 pb-1 text-center">
        <div className="px-6 py-4">
          <img
            src="https://static.thenounproject.com/png/1153028-200.png"
            alt=""
            className="w-24 h-24 mx-auto"
          />
          <div className="font-bold text-xl mb-2">Tin tức giao thông</div>
          <p className="text-gray-700 text-base">
            Cập nhật các tin tức giao thông mới nhất
          </p>
        </div>
      </div>
      {/* row 2 */}
      <div className="flex justify-center items-center max-md:hidden">
        <div className="px-6 pt-4 pb-2">
          <Link to={"/fine/motorbike"}>
            <span className="inline-block  rounded-full px-3 py-1 text-md underline text-xanh hover:text-den mb-2">
              Xem thêm
            </span>
          </Link>
        </div>
      </div>
      <div className="flex justify-center items-center max-md:hidden">
        <div className="px-6 pt-4 pb-2">
          <Link to={"/sign"}>
            <span className="inline-block  rounded-full px-3 py-1 text-md underline text-xanh hover:text-den mb-2">
              Xem thêm
            </span>
          </Link>
        </div>
      </div>
      <div className="flex justify-center items-center max-md:hidden">
        <div className="px-6 pt-4 pb-2">
          <Link>
            <span className="inline-block  rounded-full px-3 py-1 text-md underline text-xanh hover:text-den mb-2">
              Xem thêm
            </span>
          </Link>
        </div>
      </div>
      <div className="flex justify-center items-center max-md:hidden">
        <div className="px-6 pt-4 pb-2">
          <Link to={"/news"}>
            <span className="inline-block  rounded-full px-3 py-1 text-md underline text-xanh hover:text-den mb-2">
              Xem thêm
            </span>
          </Link>
        </div>
      </div>
    </div>
  );
};

export default LandRedirect;
