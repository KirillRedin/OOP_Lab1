using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Lab1
{
    class Matrix
    {
        public int matr_size;
        public int[,] matrix;

        public Matrix(int m)
        {
            this.matr_size = m;
            this.matrix = new int[m, m];
        }

        public int this[int i, int j]
        {
            get { return matrix[i, j]; }
            set { matrix[i, j] = value; }
        }

        public void CreateMatrix(System.IO.StreamReader file)
        {
            try
            {
                matrix = new int[this.matr_size, this.matr_size];
                int cnt1 = 0, cnt2 = 0;

                string[] line = file.ReadLine().Split();
                for (int i = 0; i < this.matr_size; i++)
                {
                    int[] ints = new int[matr_size];
                    for (int j = 0; j < this.matr_size; j++)
                    {
                        ints[j] = int.Parse(line[j]);
                        matrix[i, j] = ints[j];
                    }

                    if (line.Count() != this.matr_size)
                    {
                        throw new Exception("Wrong size");
                    }
                    cnt2++;
                }
                if (cnt2 != this.matr_size)
                {
                    throw new Exception("Wrong size");
                }
                LogFile.Done("Reading and creating matrix", "Done");
            }
            catch (Exception e)
            {
                Console.WriteLine("File Error");
            }
        }

    }

    class Vector
    {
        public int vect_size;
        public int[] vect;

        public int this[int i]
        {
            get { return vect[i]; }
            set { vect[i] = value; }
        }

        public Vector(int s)
        {
            this.vect_size = s;
            this.vect = new int[s];
        }

        public void CreateVect(System.IO.StreamReader file)
        {
            vect = new int[this.vect_size];

            for (int i = 0; i < this.vect_size; i++)
            {
                string line = file.ReadLine();
                vect[i] = Convert.ToInt32(line);
            }
            LogFile.Done("Reading and creating vector", "Done");
        }
    }

    class Operations
    {
        public Matrix MatrixTransp(Matrix A)
        {
            int temp;
            for (int i = 0; i < A.matr_size; i++)
                for (int j = 0; j < i; j++)
                {
                    temp = A[i, j];
                    A[i, j] = A[j, i];
                    A[j, i] = temp;
                }

            LogFile.Done("Matrix transponation", "Done");
            return A;
        }

        public bool MatrixSub(Matrix A, Matrix B)
        {
            try
            {
                if (A.matr_size != B.matr_size)
                {
                    LogFile.Fail("Matrix substraction", "Wrong Matrix size");
                    throw new Exception("Wrong size");
                }
                for (int i = 0; i < A.matr_size; i++)
                    for (int j = 0; j < A.matr_size; j++)
                        A[i, j] = A[i, j] - B[i, j];
                LogFile.Done("Matrix substraction", "Done");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Wrong Matrix Size");
                return false;
            }
        }

        public bool MatrixMult(Matrix A, Matrix B)
        {
            try
            {
                Matrix C = new Matrix(A.matr_size);
                if (A.matr_size != B.matr_size)
                {
                    LogFile.Fail("Matrix multiplication", "Wrong Matrix size");
                    throw new Exception();
                }
                for (int i = 0; i < A.matr_size; i++)
                    for (int j = 0; j < A.matr_size; j++)
                        for (int k = 0; k < B.matr_size; k++)
                            C[i, j] += A[i, k] * B[k, j];

                for (int i = 0; i < A.matr_size; i++)
                    for (int j = 0; j < A.matr_size; j++)
                        A[i, j] = C[i, j];
                LogFile.Done("Matrix multiplication", "Done");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Wrong Matrix Size");
                return false;
            }
        }

        public bool MatrixVectMult(Matrix A, Vector B)
        {
            try
            {
                if (A.matr_size != B.vect_size)
                {
                    LogFile.Fail("Matrix multiplication by Vector", "Wrong Vector size");
                }
                Vector C = new Vector(B.vect_size);
                for (int i = 0; i < A.matr_size; i++)
                    for (int j = 0; j < A.matr_size; j++)
                        C[i] += A[i, j] * B[j];

                for (int i = 0; i < A.matr_size; i++)
                    B[i] = C[i];
                LogFile.Done("Matrix multiplication by Vector", "Done");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Wrong Vector Size");
                return false;
            }
        }

        public bool VectAdd(Vector A, Vector B)
        {
            try
            {
                if (A.vect_size != B.vect_size)
                {
                    LogFile.Fail("Vector addition", "Wrong Vector size");
                }
                for (int i = 0; i < A.vect_size; i++)
                    A[i] += B[i];
                LogFile.Done("Vector addition", "Done");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Wrong Vector Size");
                return false;
            }
        }

        public Vector VectMult(Vector A, Vector B)
        {
            if (A.vect_size != B.vect_size)
            {
                LogFile.Fail("Vector multiplication", "Wrong size");
                throw new Exception("Wrong size");
            }
            Vector C = new Vector(A.vect_size);
            for (int i = 0; i < A.vect_size; i++)
                C[i] = A[i] * B[i];
            LogFile.Done("Vector multiplication", "Done");
            return C;
        }

        public int VectMaxNormalize(Vector A)
        {
            int norm = 0;
            for (int i = 0; i < A.vect_size; i++)
            {
                if (norm < Math.Abs(A[i]))
                {
                    norm = Math.Abs(A[i]);
                }
            }
            Console.Write("Max Normalize: ", norm);
            Console.WriteLine(norm);
            LogFile.Done("Max Nromalizaton =", norm.ToString());
            return norm;
        }

        public int VectINormalize(Vector A)
        {
            int norm = 0;
            for (int i = 0; i < A.vect_size; i++)
                norm += Math.Abs(A[i]);
            Console.Write("I Normalize: ");
            Console.WriteLine(norm);
            LogFile.Done("I Nromalizaton =", norm.ToString());
            return norm;
        }

        public double VectEuclidNormalize(Vector A)
        {
            double norm = 0;
            for (int i = 0; i < A.vect_size; i++)
                norm += A[i] * A[i];
            norm = Math.Sqrt(norm);
            Console.Write("Euclid Normalize: ", norm);
            Console.WriteLine(norm);
            LogFile.Done("Euclid Nromalizaton =", norm.ToString());
            return norm;
        }
    }


    class LogFile
    {
        static private StreamWriter writer;
        public static void Open()
        {
            FileStream file = new FileStream("Log.txt", FileMode.Truncate, FileAccess.Write);
            writer = new StreamWriter(file);
        }
        private static string Form(string Action, string Comment, bool res)
        {
            string report = String.Format("{0}  {1}  {2}  {3} ", DateTime.Now.ToString(), Action, Comment, (res ? "Sucsess" : "Error"));
            return report;
        }
        public static void Done(string Action, string Comment)
        {
            writer.WriteLine(Form(Action, Comment, true));
        }
        public static void Fail(string Action, string Comment)
        {
            writer.WriteLine(Form(Action, Comment, false));
        }
        public static void End()
        {
            writer.Close();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            LogFile.Open();

            StreamReader file = new StreamReader("test.txt");
            StreamReader file1 = new StreamReader("test1.txt");
            StreamReader file2 = new StreamReader("test2.txt");
            StreamReader file3 = new StreamReader("test3.txt");
            StreamReader file4 = new StreamReader("test4.txt");

            int size = Convert.ToInt32(file.ReadLine());
            int size1 = Convert.ToInt32(file1.ReadLine());

            Vector vectX = new Vector(size);
            Vector vectY = new Vector(size1);

            vectX.CreateVect(file);
            vectY.CreateVect(file1);


            int matr_size = Convert.ToInt32(file2.ReadLine());
            int matr_size1 = Convert.ToInt32(file3.ReadLine());
            int matr_size2 = Convert.ToInt32(file4.ReadLine());

            Matrix matrA = new Matrix(matr_size);
            Matrix matrB = new Matrix(matr_size1);
            Matrix matrC = new Matrix(matr_size1);

            matrA.CreateMatrix(file2);
            matrB.CreateMatrix(file3);
            matrC.CreateMatrix(file4);

            Operations op = new Operations();
            op.MatrixTransp(matrB);
            op.MatrixMult(matrA, matrB);
            op.MatrixSub(matrA, matrC);
            op.VectAdd(vectX, vectY);
            op.MatrixVectMult(matrA, vectX);

            int NormMax;
            int NormI;
            double NormEuclid;

            NormMax = op.VectMaxNormalize(vectX);
            NormI = op.VectINormalize(vectX);
            NormEuclid = op.VectEuclidNormalize(vectX);

            Console.Read();

            LogFile.Done("Calculation", "Completed");
            LogFile.End();
        }
    }
}
