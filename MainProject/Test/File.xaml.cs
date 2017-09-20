using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace MainProject.Test
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class File : Page
    {
        public File()
        {
            this.InitializeComponent();
        }

        private async void btnGetName_Click(object sender, RoutedEventArgs e)
        {
            StorageFolder pictureFolder = KnownFolders.PicturesLibrary;
            IReadOnlyList<StorageFile> pictureFileList = await pictureFolder.GetFilesAsync();
            IReadOnlyList<StorageFolder> pictureFolderList = await pictureFolder.GetFoldersAsync();

            StringBuilder picutreFolderInfo = new StringBuilder();
            foreach (StorageFile f in pictureFileList)
            {
                picutreFolderInfo.Append(f.Name + "\n");
            }
            foreach (StorageFolder f in pictureFolderList)
            {
                picutreFolderInfo.Append(f.Name + "\n");
            }
            textBlockFileName.Text = picutreFolderInfo.ToString();
        }

        private async void createFileButton_Click(object sender, RoutedEventArgs e)
        {
            // 1 创建文件
            StorageFolder folder = Windows.Storage.ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.CreateFileAsync("New Document.txt", CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, "Write text to file.");

            // 2  从文本读取文件
            StorageFolder folder2 = Windows.Storage.ApplicationData.Current.LocalFolder;
            StorageFile file2 = await folder.GetFileAsync("sample.txt");
            string text = await Windows.Storage.FileIO.ReadTextAsync(file);

            //3 使用缓冲区将字节写入到文件或从文件读取字节
            var buffer = Windows.Security.Cryptography.CryptographicBuffer.ConvertStringToBinary("There's buffer ...... ", Windows.Security.Cryptography.BinaryStringEncoding.Utf8);
            await Windows.Storage.FileIO.WriteBufferAsync(file, buffer);

            //从文件读取字节
            var buffer1 = await Windows.Storage.FileIO.ReadBufferAsync(file);
            //b.实例化DataReader，读取缓冲区
            DataReader dataReader = Windows.Storage.Streams.DataReader.FromBuffer(buffer);
            //c.从DataReader对象中读取字符串
            string text1 = dataReader.ReadString(buffer.Length);

            //使用流将文本写入文件或从文件读取文本
            //新建流，并异步地将file打开，使用可读写的方式
            var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite);
            using (var writeStream = stream.GetOutputStreamAt(0))
            {
                DataWriter dataWriter = new DataWriter(writeStream);
                dataWriter.WriteString("Stream is a good thing.");
                await dataWriter.StoreAsync();
                await writeStream.FlushAsync();
            }


            var size = stream.Size;
            using (var readStream1 = stream.GetInputStreamAt(0))
            {
                DataReader dataReader1 = new DataReader(readStream1);
                uint uintBytes = await dataReader.LoadAsync((uint)size);
                dataReader.ReadString(uintBytes);
            }

            //----------------------------
            //独立存储设置
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["key1"] = "the value";
        }
    }
}
