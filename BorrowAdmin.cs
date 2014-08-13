using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BookManage.DAL;
using BookManage.Model;

namespace BookManage.BLL
{
    public class BorrowAdmin
    {
        private DALFactory dalFactory;
        private IReaderDAL readerDAL;
        private IReaderTypeDAL readerTypeDAL;
        private IBookDAL bookDAL;
        private IBorrowDAL borrowDAL;

        public BorrowAdmin()
        {
            dalFactory = new DALFactory();
            readerDAL = dalFactory.GetReaderDAL();
            readerTypeDAL = dalFactory.GetReaderTypeDAL();
            bookDAL = dalFactory.GetBookDAL();
            borrowDAL = dalFactory.GetBorrowDAL();
        }

        public Reader GetReader(int rdID)
        {
            return (readerDAL.GetObjectByID(rdID));
        }

        public ReaderType GetReaderType(int rdType)
        {
            return (readerTypeDAL.GetObjectByID(rdType));
        }

        public DataTable GetBorrows(Reader reader)
        {
            return borrowDAL.GetBorrows(reader);
        }

        public DataTable GetBorrowsUnreturn(Reader reader)
        {
            return borrowDAL.GetBorrowsUnreturn(reader);
        }

        public DataTable GetBook(int bkID)
        {
            return bookDAL.GetBook(bkID);
        }

        public DataTable GetBooks(string bkName)
        {
            return bookDAL.GetBooks(bkName);
        }

        public Book GetBookObject(int bkID)
        {
            return bookDAL.GetObjectByID(bkID);
        }

        public bool BorrowBook(Reader reader, ReaderType readerType, Book book, Reader OperatorUser)
        {
            if (reader == null || readerType == null || book == null || OperatorUser == null)
                return false;
            if (reader.rdStatus != "有效" || (readerType.CanLendQty - reader.rdBorrowQty) <= 0) 
                return false;
            if (book.bkStatus != "在馆") return false;

            reader.rdBorrowQty++;
            readerDAL.Update(reader);

            book.bkStatus = "借出";
            bookDAL.Update(book);

            Borrow borrow = new Borrow();
            borrow.rdID = reader.rdID;
            borrow.bkID = book.bkID;
            borrow.ldContinueTimes = 0;
            borrow.ldDateOut = DateTime.Now;
            borrow.ldDateRetPlan = DateTime.Now.AddDays(readerType.CanLendDay);
            borrow.lsHasReturn = false;
            borrow.OperatorLend = OperatorUser.rdName;
            borrowDAL.Add(borrow);
            return true;
        }

        public int BorrowBook(int rdID, int bkID, string OperatorLend)
        {
            return borrowDAL.BorrowBook(rdID, bkID, OperatorLend);
        }
    }
}
