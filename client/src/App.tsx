import { useState } from 'react'
import { hello, notFound } from './api/api'
import { ApiError } from './library/responses.ts'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'

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

  return (
    <>
      <div>
        <a href="https://vite.dev" target="_blank">
          <img src={viteLogo} className="logo" alt="Vite logo" />
        </a>
        <a href="https://react.dev" target="_blank">
          <img src={reactLogo} className="logo react" alt="React logo" />
        </a>
      </div>
      <h1>Vite + React</h1>
      <div className="card">
        <button onClick={() => setCount((count) => count + 1)}>
          count is {count}
        </button>
        <p>
          Edit <code>src/App.tsx</code> and save to test HMR
        </p>
      </div>
      <p className="read-the-docs">
        Click on the Vite and React logos to learn more
      </p>
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
    </>
  )
}

export default App
