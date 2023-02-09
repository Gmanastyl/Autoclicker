using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace Autoclicker_v1.Views;

public partial class StuMainWindow : Window 
{
    private bool imagePressed;
    private Point clickPosition;
    private Image? selectedImage;

    public StuMainWindow() 
    {
        InitializeComponent();
    }

    private void LeftSliderImage_MouseLeftButtonDown(object sender, PointerPressedEventArgs e)
    {
        if (sender is Image)
        {

        

        imagePressed = true;

            clickPosition = e.GetPosition(leftSlider);

            leftSlider.PointerMoved += LeftSliderImage_Moved;

            selectedImage = sender as Image;


            

        }
    }

    private void LeftSliderImage_Moved(object sender, PointerEventArgs e)
    {



        if (imagePressed)
        {
            var xpos = e.GetPosition(leftSlider).X;
            var srml = selectedImage.Margin.Left;

            var initialPointerPositionLocationInRectangleOffset =
                e.GetPosition(leftSlider).X - selectedImage.Margin.Left;

            Point mousePosition = e.GetPosition(leftSlider);

            var m = mousePosition.X;

            var distance = mousePosition.X - clickPosition.X;

            var left = clickPosition.X + distance;

            var x = (double)leftSlider.Width;

            if (left > leftSlider.Width - selectedImage.Width) left = leftSlider.Width - selectedImage.Width;

            if (left < 0) left = 0;

            var leftOffset = left;

            selectedImage.Margin = new Thickness(
                left,
                selectedImage.Margin.Top,
                selectedImage.Margin.Right,
                selectedImage.Margin.Bottom);
        }

    }

    private void LeftSliderImage_MouseLeftButtonUp(object sender, PointerReleasedEventArgs e)
    {
        imagePressed = false;
    }

    private void RightSliderImage_MouseLeftButtonDown(object sender, PointerPressedEventArgs e)
    {
        if (sender is Image)
        {



            imagePressed = true;

            clickPosition = e.GetPosition(leftSlider);

            rightSlider.PointerMoved += RightSliderImage_Moved;

            selectedImage = sender as Image;




        }
    }

    private void RightSliderImage_Moved(object sender, PointerEventArgs e)
    {



        if (imagePressed)
        {
            var xpos = e.GetPosition(leftSlider).X;
            var srml = selectedImage.Margin.Left;

            var initialPointerPositionLocationInRectangleOffset =
                e.GetPosition(leftSlider).X - selectedImage.Margin.Left;

            Point mousePosition = e.GetPosition(rightSlider);

            var m = mousePosition.X;

            var distance = mousePosition.X - clickPosition.X;

            var left = clickPosition.X + distance;

            var x = (double)rightSlider.Width;

            if (left > rightSlider.Width - selectedImage.Width) left = rightSlider.Width - selectedImage.Width;

            if (left < 0) left = 0;

            var leftOffset = left;

            selectedImage.Margin = new Thickness(
                left,
                selectedImage.Margin.Top,
                selectedImage.Margin.Right,
                selectedImage.Margin.Bottom);
        }

    }

    private void RightSliderImage_MouseLeftButtonUp(object sender, PointerReleasedEventArgs e)
    {
        imagePressed = false;
    }


    private void HideButton_Click(object sender, RoutedEventArgs e) {
        this.WindowState = WindowState.Minimized;
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e) {
        this.Close();
    }

    private void MaximizeButton_Click(object sender, RoutedEventArgs e) {
        if (this.WindowState == WindowState.Maximized)
        {
            this.WindowState = WindowState.Normal;
        }
        else
        {
            this.WindowState = WindowState.Maximized;
        }
    }


    private void Window_PointerPressed(object sender, PointerPressedEventArgs e) {
        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
        {
            this.BeginMoveDrag(e);
        }
    }
}