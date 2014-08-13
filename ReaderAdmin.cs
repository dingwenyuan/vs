using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using BookManage.DAL;
using BookManage.Model;

namespace BookManage.BLL
{
    public class ReaderAdmin
    {
        private DALFactory dalFactory;
        private IReaderDAL readerDAL;
        private IReaderTypeDAL readerTypeDAL;
        private IBorrowDAL borrowDAL;

        public ReaderAdmin()
        {
            dalFactory = new DALFactory();
            readerDAL = dalFactory.GetReaderDAL();
            readerTypeDAL = dalFactory.GetReaderTypeDAL();
            borrowDAL = dalFactory.GetBorrowDAL();
        }

        public Reader GetReader(int rdID)
        {
            return (readerDAL.GetObjectByID(rdID));
        }

        public DataTable GetReader(int rdType, string rdDept, string rdName)
        {
            return (readerDAL.GetReader(rdType, rdDept, rdName));
        }

        public int Add(Reader reader)
        {
            return (readerDAL.Add(reader));
        }

        public int Update(Reader reader)
        {
            return (readerDAL.Update(reader));
        }

        public ReaderType GetReaderType(int rdType)
        {
            return (readerTypeDAL.GetObjectByID(rdType));
        }

        public DataTable GetAllReaderType()
        {
            return (readerTypeDAL.GetAll());
        }

        public DataTable GetBorrows(Reader reader)
        {
            return borrowDAL.GetBorrows(reader);
        }
    }
}
