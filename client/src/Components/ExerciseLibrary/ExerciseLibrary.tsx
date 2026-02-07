import { Box, Button, Typography } from "@mui/material";
import { useExerciseLibraryContext } from "../../Contexts/ExerciseLibraryContext";

export default function ExerciseLibrary() {

  const { counter, setCounter } = useExerciseLibraryContext();

  return (
    <Box>
      <Typography variant="h2">The ExData Library</Typography>
      <p>{counter}</p>
      <Button onClick={() => setCounter(counter + 1)}>Push</Button>
    </Box>
  )
}
