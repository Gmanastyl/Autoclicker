using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace Autoclicker_v1.Views;

public partial class StuMainWindow : Window
{
    // Flag indicating if when the mouse button was pressed, it was on an image
    private bool imagePressed;

    // The position on the canvas where the mouse button was clicked
    private Point clickPosition;

    // The image that was clicked on
    private Image? selectedImage;

    // Flag indicating if the arrows on the slider should be able to cross each other
    private bool arrowsShouldCross = false;


    // 
    private double leftClickRightArrowLeftMarginMin = 0;
    private double leftClickRightArrowMarginLeftMax = 145; 

    private double rightClickLeftArrowLeftMarginMin = 0;
    private double rightClickRightArrowMarginLeftMax = 145;


    public StuMainWindow()
    {
        InitializeComponent();

    }

    private void LeftSliderImage_MouseLeftButtonDown(object sender, PointerPressedEventArgs e)
    {
        if (sender is Image)
        {
            selectedImage = sender as Image;

            imagePressed = true;

            clickPosition = e.GetPosition(leftClickSlider);

            leftClickSlider.PointerMoved += LeftSliderImage_Moved;

            

        }
    }

    private void LeftSliderImage_Moved(object sender, PointerEventArgs e)
    {

        if (imagePressed)
        {
            var xpos = e.GetPosition(leftClickSlider).X;
            var srml = selectedImage.Margin.Left;

            var initialPointerPositionLocationInRectangleOffset =
                e.GetPosition(leftClickSlider).X - selectedImage.Margin.Left;

            Point mousePosition = e.GetPosition(leftClickSlider);

            var m = mousePosition.X;

            var distance = mousePosition.X - clickPosition.X;

            var left = clickPosition.X + distance;

            var x = (double)leftClickSlider.Width;

            if (left > leftClickSlider.Width - selectedImage.Width) left = leftClickSlider.Width - selectedImage.Width;

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

            clickPosition = e.GetPosition(leftClickSlider);

            rightClickSlider.PointerMoved += RightSliderImage_Moved;

            selectedImage = sender as Image;

        }
    }

    private void RightSliderImage_Moved(object sender, PointerEventArgs e)
    {

        if (imagePressed)
        {
            var xpos = e.GetPosition(leftClickSlider).X;
            var srml = selectedImage.Margin.Left;

            var initialPointerPositionLocationInRectangleOffset =
                e.GetPosition(leftClickSlider).X - selectedImage.Margin.Left;

            Point mousePosition = e.GetPosition(rightClickSlider);

            var m = mousePosition.X;

            var distance = mousePosition.X - clickPosition.X;

            var left = clickPosition.X + distance;

            var x = (double)rightClickSlider.Width;

            if (left > rightClickSlider.Width - selectedImage.Width) left = rightClickSlider.Width - selectedImage.Width;

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


    private void HideButton_Click(object sender, RoutedEventArgs e)
    {
        this.WindowState = WindowState.Minimized;
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void MaximizeButton_Click(object sender, RoutedEventArgs e)
    {
        if (this.WindowState == WindowState.Maximized)
        {
            this.WindowState = WindowState.Normal;
        }
        else
        {
            this.WindowState = WindowState.Maximized;
        }
    }


    private void Window_PointerPressed(object sender, PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
        {
            this.BeginMoveDrag(e);
        }
    }
}