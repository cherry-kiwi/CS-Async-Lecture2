using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Async_Lecture2
{
    public partial class Form1 : Form
    {
        //비동기 취소: CancellationTokenSource
        //비동기 취소 여부 추적: CancellationToken

        CancellationTokenSource tokenSource;

        public Form1()
        {
            InitializeComponent();
        }

        ////비동기 취소
        //private async Task DoWorkAsync(CancellationToken token)
        //{
        //    while (!token.IsCancellationRequested) //무한루프
        //    {
        //        await Task.Delay(100); //딜레이
        //        int.TryParse(lblIndex.Text, out var value);
        //        lblIndex.Text = (++value).ToString();
        //    }
        //}

        //비동기 취소 추적
        private async Task DoWorkAsync(CancellationToken token)
        {
            while (true) //무한루프
            {
                token.ThrowIfCancellationRequested(); //Cancel()이 호출이 되면 에러를 발생시킴
                await Task.Delay(100); //딜레이
                int.TryParse(lblIndex.Text, out var value);
                lblIndex.Text = (++value).ToString();
            }
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            tokenSource = new CancellationTokenSource(); //객체생성
            var token = tokenSource.Token;

            //비동기 취소 추적
            try
            {
                await DoWorkAsync(token);
            }
            catch (OperationCanceledException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            ////비동기 취소
            //await DoWorkAsync(token);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            tokenSource.Cancel();
        }
    }
}
