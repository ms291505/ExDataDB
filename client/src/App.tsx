import { useState } from 'react'
import { hello, notFound, getExercises, getExercisesPaginated, getExercisesCursor } from './api/api'
import { ApiError } from './library/responses.ts'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import ExerciseLibrary from './Components/ExerciseLibrary/ExerciseLibrary.tsx'
import { Box, Container } from '@mui/material'
import { CenteredColumn } from './Components/StyledComponents/CenterColumn.tsx'

function App() {
  const [count, setCount] = useState(0)

  const [message, setMessage] = useState("");
  const [errMessage, setErrMessage] = useState("");

  async function testIt() {
    const response = await hello();
    setMessage(response.message);
  }

  async function errorTest() {
    try {

      await notFound();
    } catch (err) {
      if (err instanceof ApiError) {
        setErrMessage(err.message);
      } else {
        throw new Error("Unkown error occured.");
      }
    }
  }

  async function exercises() {
    await getExercises();
  }
  async function exercisesPaginated() {
    const data = await getExercisesPaginated(1, 10);
    console.log(data.items);

  }
  async function exercisesCursor() {
    const data2 = await getExercisesCursor(25, 29, "Arm Circles");
    console.log(data2.items);
    console.log(data2);

  }

  return (
    <CenteredColumn>
      <ExerciseLibrary />
      <div>
        <button
          onClick={testIt}
        >
          Test This Bad Boy!
        </button>
        {
          message
            ? <p>{message}</p>
            : <p>"Waiting to test."</p>
        }
      </div>
      <div>
        <button
          onClick={errorTest}
        >
          Test This Bad Boy!
        </button>
        {
          errMessage
            ? <p>{errMessage}</p>
            : <p>"Waiting to test."</p>
        }
      </div>
      <div>
        <button
          onClick={exercises}
        >
          Test This Bad Boy!
        </button>
        {
          errMessage
            ? <p>{errMessage}</p>
            : <p>"Waiting to test."</p>
        }
      </div>
      <div>
        <button
          onClick={exercisesPaginated}
        >
          Test This Bad Boy!
        </button>
        {
          errMessage
            ? <p>{errMessage}</p>
            : <p>"Waiting to test."</p>
        }
      </div>
      <div>
        <button
          onClick={exercisesCursor}
        >
          Test This Bad Boy!
        </button>
        {
          errMessage
            ? <p>{errMessage}</p>
            : <p>"Waiting to test."</p>
        }
      </div>
    </CenteredColumn>
  )
}

export default App
