using System;

namespace CursesWork
{
    public class Sort
    {
        public static void Quicksort(int[] arr, int start, int end, ref int eqCount, ref int changeCount)
        {
            if (start >= end)
            {
                return;
            }
            int pivot = Partition(arr, start, end, ref eqCount, ref changeCount);
            Quicksort(arr, start, pivot - 1, ref eqCount, ref changeCount);
            Quicksort(arr, pivot + 1, end, ref eqCount, ref changeCount);
        }

        private static int Partition(int[] array, int start, int end, ref int eqCount, ref int changeCount)
        {
            int temp;
            int marker = start;
            for (int i = start; i < end; i++)
            {
                eqCount++;
                if (array[i] < array[end]) // array[end] is pivot
                {
                    changeCount++;
                    temp = array[marker]; // swap
                    array[marker] = array[i];
                    array[i] = temp;
                    marker += 1;
                }
            }

            temp = array[marker];
            array[marker] = array[end];
            array[end] = temp;
            return marker;
        }

        public static void IntroSort(int[] data, ref int eqCount, ref int changeCount)
        {
            int partitionSize = Partition(ref data, 0, data.Length - 1, ref eqCount, ref changeCount);

            if (partitionSize < 16)
            {
                InsertionSort(ref data, ref eqCount, ref changeCount);
            }
            else if (partitionSize > (2 * Math.Log(data.Length)))
            {
                HeapSort(ref data, ref eqCount, ref changeCount);
            }
            else
            {
                QuickSortRecursive(ref data, 0, data.Length - 1, ref eqCount, ref changeCount);
            }
        }

        private static void InsertionSort(ref int[] data, ref int eqCount, ref int changeCount)
        {
            for (int i = 1; i < data.Length; ++i)
            {
                int j = i;

                while ((j > 0))
                {
                    eqCount++;
                    if (data[j - 1] > data[j])
                    {
                        changeCount++;
                        data[j - 1] ^= data[j];
                        data[j] ^= data[j - 1];
                        data[j - 1] ^= data[j];

                        --j;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private static void HeapSort(ref int[] data, ref int eqCount, ref int changeCount)
        {
            int heapSize = data.Length;

            for (int p = (heapSize - 1) / 2; p >= 0; --p)
                MaxHeapify(ref data, heapSize, p, ref eqCount, ref changeCount);

            for (int i = data.Length - 1; i > 0; --i)
            {
                changeCount++;
                int temp = data[i];
                data[i] = data[0];
                data[0] = temp;

                --heapSize;
                MaxHeapify(ref data, heapSize, 0, ref eqCount, ref changeCount);
            }
        }

        private static void MaxHeapify(ref int[] data, int heapSize, int index, ref int eqCount, ref int changeCount)
        {
            int left = (index + 1) * 2 - 1;
            int right = (index + 1) * 2;
            int largest = 0;

            eqCount++;
            if (left < heapSize && data[left] > data[index])
                largest = left;
            else
                largest = index;

            eqCount++;
            if (right < heapSize && data[right] > data[largest])
                largest = right;

            eqCount++;
            if (largest != index)
            {
                changeCount++;
                int temp = data[index];
                data[index] = data[largest];
                data[largest] = temp;

                MaxHeapify(ref data, heapSize, largest, ref eqCount, ref changeCount);
            }
        }

        private static void QuickSortRecursive(ref int[] data, int left, int right, ref int eqCount, ref int changeCount)
        {
            if (left < right)
            {
                int q = Partition(ref data, left, right, ref eqCount, ref changeCount);
                QuickSortRecursive(ref data, left, q - 1, ref eqCount, ref changeCount);
                QuickSortRecursive(ref data, q + 1, right, ref eqCount, ref changeCount);
            }
        }

        private static int Partition(ref int[] data, int left, int right, ref int eqCount, ref int changeCount)
        {
            int pivot = data[right];
            int temp;
            int i = left;

            for (int j = left; j < right; ++j)
            {
                eqCount++;
                if (data[j] <= pivot)
                {
                    changeCount++;
                    temp = data[j];
                    data[j] = data[i];
                    data[i] = temp;
                    i++;
                }
            }

            data[right] = data[i];
            data[i] = pivot;

            return i;
        }
    }
}
