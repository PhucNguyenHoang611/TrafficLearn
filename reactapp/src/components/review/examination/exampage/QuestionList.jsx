import React, { useEffect } from 'react'
import QuestionItem from './QuestionItem';

const QuestionList = ({ questions, setQuestions, scrollQuestion }) => {
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
          <div key={index} id={`question-${item.questionNumber}`}>
            <QuestionItem
              question={item}
              questions={questions}
              setQuestions={setQuestions} />
          </div>
        );
      })}
    </div>
  )
}

export default QuestionList;