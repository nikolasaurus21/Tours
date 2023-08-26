import React from "react";
import "./pagination.css";
import { AiOutlineArrowLeft, AiOutlineArrowRight } from "react-icons/ai";
import { usePagination } from "../context/PaginationContext";
interface PaginationProps {
  totalPages: number;
}

const Pagination = ({ totalPages }: PaginationProps) => {
  const { currentPage, setCurrentPage } = usePagination();

  if (totalPages === 0) {
    return null;
  }
  return (
    <div className="paginations">
      <button
        disabled={currentPage === 1}
        onClick={() => setCurrentPage(currentPage - 1)}
      >
        <AiOutlineArrowLeft />
      </button>
      <span>{`  ${currentPage} / ${totalPages} `}</span>
      <button
        disabled={currentPage === totalPages}
        onClick={() => setCurrentPage(currentPage + 1)}
      >
        <AiOutlineArrowRight />
      </button>
    </div>
  );
};

export default Pagination;
