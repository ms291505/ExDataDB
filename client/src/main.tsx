import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import App from './App.tsx'
import { BrowserRouter, Navigate, Route, Routes } from 'react-router'
import { Container, CssBaseline } from '@mui/material'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <CssBaseline />
    <Container sx={{ mt: 2 }}>
      <BrowserRouter>
        <Routes>
          <Route index element={<App />} />
          <Route path="*" element={<Navigate to="/" replace />} />
        </Routes>
      </BrowserRouter>
    </Container>
  </StrictMode >
)
