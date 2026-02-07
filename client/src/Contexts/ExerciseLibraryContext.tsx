import { createContext, useContext, useState, type ReactNode } from "react";
import type { Exercise } from "../library/types";

type ProviderProps = {
  children: ReactNode
};

type ExerciseLibraryContextValue = {
  exercises: Exercise[];
  setExercises: (value: Exercise[]) => void;
  counter: number;
  setCounter: (value: number) => void;
};

const ExerciseLibraryContext = createContext<ExerciseLibraryContextValue>({
  exercises: [],
  setExercises: () => { },
  counter: 0,
  setCounter: () => { }
});

export default function ExerciseLibraryContextProvider({ children }: ProviderProps) {

  const [exercises, setExercises] = useState<Array<Exercise>>([]);
  const [counter, setCounter] = useState(0);

  return (
    <ExerciseLibraryContext.Provider value={{
      exercises, setExercises, counter, setCounter
    }}>
      {children}
    </ExerciseLibraryContext.Provider>
  )
}

export function useExerciseLibraryContext() {
  return useContext(ExerciseLibraryContext);
}
