import { ReactNode } from "react";

type Props = {
  children: ReactNode
}

const queryClient = new QueryClient();

export default function QueryClientProvider({ children }: Props)
