import ExerciseLibrary from './Components/ExerciseLibrary/ExerciseLibrary.tsx'
import { CenteredColumn } from './Components/StyledComponents/CenterColumn.tsx'
import { QueryClient, QueryClientProvider } from '@tanstack/react-query'

function App() {
  const queryClient = new QueryClient();

  return (
    <CenteredColumn>
      <QueryClientProvider client={queryClient}>
        <ExerciseLibrary />
      </QueryClientProvider>
    </CenteredColumn>
  )
}

export default App
