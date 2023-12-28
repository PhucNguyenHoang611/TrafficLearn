import React, { useEffect } from 'react'
import ReviewQuestionItem from './ReviewQuestionItem';

const ReviewQuestionList = ({ questions, scrollQuestion }) => {
  const handleChangeQuestion = (questionNumber) => {
    const questionCard = document.getElementById(`question-${questionNumber}`);
    if (questionCard) {
      questionCard.scrollIntoView({ behavior: "smooth" });
    }
  };

  useEffect(() => {
    if (scrollQuestion != 0) {
      handleChangeQuestion(scrollQuestion);
    }
  }, [scrollQuestion]);
  
  return (
    <div className="ml-4">
      {questions.map((item, index) => {
        return (
          <div key={index} id={`question-${index + 1}`}>
            <ReviewQuestionItem index={index + 1} question={item} />
          </div>
        );
      })}
    </div>
  )
}

export default ReviewQuestionList;