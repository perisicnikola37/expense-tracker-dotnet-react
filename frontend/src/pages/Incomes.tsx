import { useEffect } from "react";

import { Helmet } from "react-helmet";

import { useModal } from "../contexts/GlobalContext";
import useObjects from "../hooks/GlobalHooks/useObjects";
import EnhancedTable from "../components/Tables/IncomesTable";
import { IncomeInterface } from "../interfaces/globalInterfaces";

const Incomes = () => {
  const { objects, loadObjects } = useObjects<IncomeInterface>();
  const { actionChange, appliedFilters } = useModal();

  useEffect(() => {
    loadObjects(appliedFilters, "income");
    window.scrollTo({
      top: 0,
    });
  }, [actionChange, appliedFilters]);

  return (
    <div className="w-full max-w-screen-xl min-h-[48rem] mx-auto p-4 md:py-8">
      <Helmet>
        <title>Incomes | Expense Tracker</title>
      </Helmet>
      <EnhancedTable incomes={objects} rowsPerPage={5} />
    </div>
  );
};

export default Incomes;
