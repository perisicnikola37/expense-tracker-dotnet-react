import { useEffect } from "react";

import { Outlet } from "react-router-dom";

import { CssBaseline, ThemeProvider } from "@mui/material";

import NavBar from "./components/NavBar";
import Footer from "./components/Footer";
import useScrollToTop from "./hooks/useScrollToTop";
import { useLoading } from "./contexts/LoadingContext";
import { getTheme } from "./config/themeConfiguration";
import { useDarkMode } from "./contexts/DarkModeContext";
import LoadingContainer from "./components/LoadingContainer";
import ScrollToTopButton from "./components/ScrollToTopButton";
import { useAuthenticationMiddleware } from "./middleware/authMiddleware";

function App() {
  const { loading, setLoadingState } = useLoading();
  const { darkMode } = useDarkMode();
  const authenticate = useAuthenticationMiddleware();
  const theme = getTheme(darkMode);
  const { showScrollToTop, scrollToTop } = useScrollToTop();

  // Authentication middleware
  useEffect(() => {
    authenticate();
  }, [authenticate]);

  useEffect(() => {
    const timeout = setTimeout(() => {
      setLoadingState(false);
    }, 600);

    return () => clearTimeout(timeout);
  }, [setLoadingState]);

  return (
    <div
      className={
        darkMode
          ? "flex flex-col min-h-screen bg-[#111827]"
          : "flex flex-col min-h-screen"
      }
    >
      <div className="flex-grow">
        <LoadingContainer loading={loading}>
          <div className="flex flex-col min-h-full">
            <ThemeProvider theme={theme}>
              <CssBaseline />
              <NavBar />
              <Outlet />
              {showScrollToTop && <ScrollToTopButton onClick={scrollToTop} />}
              <Footer />
            </ThemeProvider>
          </div>
        </LoadingContainer>
      </div>
    </div>
  );
}

export default App;
