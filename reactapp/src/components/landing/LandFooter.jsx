import car from "/car.png?url";
import { LazyLoadImage } from "react-lazy-load-image-component";

const LandFooter = () => {
  return (
    <div>
      {" "}
      <div className="flex max-sm:flex-col items-center sm:justify-center w-full">
        <div className="max-w-[50rem] min-w-[20rem]">
          <LazyLoadImage
            src={car}
            alt="Traffic Landing Image"
            effect="blur"
            className="h-auto w-full max-w-screen-lg"
          />
        </div>
        <div>
          <h2 className="font-bold text-lg">
            Liên hệ với chúng tôi thông qua các kênh sau
          </h2>
          <p>
            <span className="font-bold">Email: </span>
            <span>
              <a href="mailto:trafficlearnteam@gmail.com">trafficlearnteam@gmail.com</a>
            </span>
          </p>
        </div>
      </div>
    </div>
  );
};

export default LandFooter;
