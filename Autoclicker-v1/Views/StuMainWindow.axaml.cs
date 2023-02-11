using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Chrome;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.Themes.Default;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;

namespace Autoclicker_v1.Views;

public partial class StuMainWindow : Window
{
    #region Private Members

    // Flag indicating if when the mouse button was pressed, it was on an image
    private bool imagePressed;

    // The position on the canvas where the mouse button was clicked
    private Point pointerPressedPosition;

    // The image that was clicked on
    private Image? selectedImage;

    private Image converseImage;

    // Flag indicating if the arrows on the slider should be able to cross each other
    private bool shouldArrowsCross = false;

    // The minimum the left click arrows can slide
    private double leftClickArrowLeftMarginMin = 0;

    // The maximum the left click arrows can slide
    private double leftClickArrowLeftMarginMax = 150;

    // The minimum the left click arrows can slide
    private double rightClickArrowLeftMarginMin = 0;

    // The maximum the left click arrows can slide
    private double rightClickArrowLeftMarginMax = 150;

    // The left margin value when the image was pressed on
    private double selectedImageOriginalLeftMargin;

    public double RightArrowValue { get; set; } = 0;

    public double LeftArrowValue { get; set; } = 150;


    #endregion EndRegion - Private Members


    #region Constructors

    /// <summary>
    /// Default Constructor
    /// </summary>
    public StuMainWindow()
    {
        InitializeComponent();

        DataContext= this;

    }

    #endregion EndRegion - Constructors


    #region Private Methods

    /// <summary>
    /// This method is called when either of the arrows in the left click slider is clicked
    /// </summary>
    /// <param name="sender">The object in the view that was clicked on</param>
    /// <param name="e">The pointer args</param>
    private void LeftClick_SliderImage_PointerPressed(object sender, PointerPressedEventArgs e)
    {
        // Check to see if the object that was clicked on is actually an image
        if (sender is Image)
        {
            // Set the selected image to the image pressed on
            selectedImage = sender as Image;

            if (selectedImage == leftClickLeftArrow)
                converseImage = leftClickRightArrow;

            else
                converseImage = leftClickLeftArrow;

            // Set the Flag to indicate an image has been pressed on
            imagePressed = true;

            // Get the pointer position when the image was pressed on
            pointerPressedPosition = e.GetPosition(leftClickSlider);

            // Create an event to call the method when the pointer moves
            leftClickSlider.PointerMoved += LeftClick_SliderImage_PointerMoved;

        }
    }

    /// <summary>
    /// This method is called when the pointer is released
    /// The parameters of the method should be able to be removed
    /// </summary>
    /// <param name="sender">Not used</param>
    /// <param name="e">Not used</param>
    private void LeftClick_SliderImage_PointerReleased(object sender, PointerReleasedEventArgs e)
    {
        // Set the image pressed Flag to false
        imagePressed = false;
    }

    /// <summary>
    /// Called when the pointer moves
    /// This is called multiple times during a slide
    /// </summary>
    /// <param name="sender">Not used</param>
    /// <param name="e">The pointer args</param>
    private void LeftClick_SliderImage_PointerMoved(object sender, PointerEventArgs e)
    {
        // If we have already clicked on an image...
        if (imagePressed)
        {
            // Get the pointer position 
            Point mousePosition = e.GetPosition(leftClickSlider);

            // Calculate the distance the pointer has moved from the original pointer position
            var distance = mousePosition.X - pointerPressedPosition.X;

            // Calculate the new left margin for the image
            var newSelectedImageMarginValue = pointerPressedPosition.X + distance;

            double newConverseImageMarginValue = converseImage.Margin.Left;

            // If the selected image is a left arrow...
            if (selectedImage == leftClickLeftArrow)
            {
                // If the new margin for the selected image is less than the minimum...
                if (newSelectedImageMarginValue < leftClickArrowLeftMarginMin)
                    newSelectedImageMarginValue = leftClickArrowLeftMarginMin;

                // If the new margin for the selected image is greater than the maximum
                if (newSelectedImageMarginValue > leftClickArrowLeftMarginMax - selectedImage.Width - converseImage.Width)
                {
                    // Set the selected image margin to the maximum
                    newSelectedImageMarginValue = leftClickArrowLeftMarginMax - selectedImage.Width - converseImage.Width;

                    newConverseImageMarginValue = leftClickArrowLeftMarginMax - converseImage.Width;
                }

                if (newSelectedImageMarginValue + selectedImage.Width > converseImage.Margin.Left)
                    newConverseImageMarginValue = newSelectedImageMarginValue + selectedImage.Width;

                newSelectedImageMarginValue = Math.Round(newSelectedImageMarginValue, 1);

                newConverseImageMarginValue = Math.Round(newConverseImageMarginValue,1);



                converseImage.Margin = new Thickness(
                       newConverseImageMarginValue,
                       selectedImage.Margin.Top,
                       selectedImage.Margin.Right,
                       selectedImage.Margin.Bottom);


                selectedImage.Margin = new Thickness(
                       newSelectedImageMarginValue,
                       selectedImage.Margin.Top,
                       selectedImage.Margin.Right,
                       selectedImage.Margin.Bottom);
            }



            // If the selected image is a right arrow...
            if (selectedImage == leftClickRightArrow)
            {
                // If the new margin for the selected image is greater than the maximum...
                if (newSelectedImageMarginValue > leftClickArrowLeftMarginMax - selectedImage.Width)
                    newSelectedImageMarginValue = leftClickArrowLeftMarginMax - selectedImage.Width;

                // If the new margin for the selected image is less than the minimum
                if (newSelectedImageMarginValue < leftClickArrowLeftMarginMin + converseImage.Width)
                {
                    // Set the selected image margin to the minimum
                    newSelectedImageMarginValue = leftClickArrowLeftMarginMin + converseImage.Width;

                    newConverseImageMarginValue = leftClickArrowLeftMarginMin;
                }

                // If the the right arrow margin is less than the left image margin
                if (newSelectedImageMarginValue < newConverseImageMarginValue + converseImage.Width)
                    newConverseImageMarginValue = newSelectedImageMarginValue - converseImage.Width;

                newSelectedImageMarginValue = Math.Round(newSelectedImageMarginValue, 1);

                newConverseImageMarginValue = Math.Round(newConverseImageMarginValue, 1);

                converseImage.Margin = new Thickness(
                       newConverseImageMarginValue,
                       selectedImage.Margin.Top,
                       selectedImage.Margin.Right,
                       selectedImage.Margin.Bottom);


                selectedImage.Margin = new Thickness(
                       newSelectedImageMarginValue,
                       selectedImage.Margin.Top,
                       selectedImage.Margin.Right,
                       selectedImage.Margin.Bottom);
            }
            LeftArrowValue = newSelectedImageMarginValue;

            RightArrowValue = newSelectedImageMarginValue;


           

        }

    }




    /// <summary>
    /// This method is called when either of the arrows in the right click slider is clicked
    /// </summary>
    /// <param name="sender">The object in the view that was clicked on</param>
    /// <param name="e">The pointer args</param>
    private void RightClick_SliderImage_PointerPressed(object sender, PointerPressedEventArgs e)
    {
        // Check to see if the object that was clicked on is actually an image
        if (sender is Image)
        {
            // Set the selected image to the image pressed on
            selectedImage = sender as Image;

            // Set the Flag to indicate an image has been pressed on
            imagePressed = true;

            // Get the pointer position when the image was pressed on
            pointerPressedPosition = e.GetPosition(rightClickSlider);

            // Create an event to call the method when the pointer moves
            rightClickSlider.PointerMoved += RightClick_SliderImage_PointerMoved;

            if (selectedImage == rightClickLeftArrow)
                converseImage = rightClickRightArrow;

            else
                converseImage = rightClickLeftArrow;

        }



    }
    /// <summary>
    /// This method is called when the pointer is released
    /// The parameters of the method should be able to be removed
    /// </summary>
    /// <param name="sender">Not used</param>
    /// <param name="e">Not used</param>
    private void RightClick_SliderImage_PointerReleased(object sender, PointerReleasedEventArgs e)
    {
        // Set the image pressed Flag to false
        imagePressed = false;
    }

    /// <summary>
    /// Called when the pointer moves
    /// This is called multiple times during a slide
    /// </summary>
    /// <param name="sender">Not used</param>
    /// <param name="e">The pointer args</param>
    private void RightClick_SliderImage_PointerMoved(object sender, PointerEventArgs e)
    {
        // If we have already clicked on an image...
        if (imagePressed)
        {
            // Get the pointer position 
            Point mousePosition = e.GetPosition(rightClickSlider);

            // Calculate the distance the pointer has moved from the original pointer position
            var distance = mousePosition.X - pointerPressedPosition.X;

            // Calculate the new left margin for the image
            var newSelectedImageMarginValue = pointerPressedPosition.X + distance;

            double newConverseImageMarginValue = converseImage.Margin.Left;

            // If the selected image is a left arrow...
            if (selectedImage == rightClickLeftArrow)
            {
                // If the new margin for the selected image is less than the minimum...
                if (newSelectedImageMarginValue < rightClickArrowLeftMarginMin)
                    newSelectedImageMarginValue = rightClickArrowLeftMarginMin;

                // If the new margin for the selected image is greater than the maximum
                if (newSelectedImageMarginValue > rightClickArrowLeftMarginMax - selectedImage.Width - converseImage.Width)
                {
                    // Set the selected image margin to the maximum
                    newSelectedImageMarginValue = rightClickArrowLeftMarginMax - selectedImage.Width - converseImage.Width;

                    newConverseImageMarginValue = rightClickArrowLeftMarginMax - converseImage.Width;
                }

                if (newSelectedImageMarginValue + selectedImage.Width > converseImage.Margin.Left)
                    newConverseImageMarginValue = newSelectedImageMarginValue + selectedImage.Width;

                newSelectedImageMarginValue = Math.Round(newSelectedImageMarginValue, 1);

                newConverseImageMarginValue = Math.Round(newConverseImageMarginValue, 1);

                converseImage.Margin = new Thickness(
                       newConverseImageMarginValue,
                       selectedImage.Margin.Top,
                       selectedImage.Margin.Right,
                       selectedImage.Margin.Bottom);


                selectedImage.Margin = new Thickness(
                       newSelectedImageMarginValue,
                       selectedImage.Margin.Top,
                       selectedImage.Margin.Right,
                       selectedImage.Margin.Bottom);
            }



            // If the selected image is a right arrow...
            if (selectedImage == rightClickRightArrow)
            {
                // If the new margin for the selected image is greater than the maximum...
                if (newSelectedImageMarginValue > rightClickArrowLeftMarginMax - selectedImage.Width)
                    newSelectedImageMarginValue = rightClickArrowLeftMarginMax - selectedImage.Width;

                // If the new margin for the selected image is less than the minimum
                if (newSelectedImageMarginValue < rightClickArrowLeftMarginMin + converseImage.Width)
                {
                    // Set the selected image margin to the minimum
                    newSelectedImageMarginValue = rightClickArrowLeftMarginMin + converseImage.Width;

                    newConverseImageMarginValue = rightClickArrowLeftMarginMin;
                }

                // If the the right arrow margin is less than the left image margin
                if (newSelectedImageMarginValue < newConverseImageMarginValue + converseImage.Width)
                    newConverseImageMarginValue = newSelectedImageMarginValue - converseImage.Width;

                newSelectedImageMarginValue = Math.Round(newSelectedImageMarginValue, 1);

                newConverseImageMarginValue = Math.Round(newConverseImageMarginValue, 1);

                converseImage.Margin = new Thickness(
                       newConverseImageMarginValue,
                       selectedImage.Margin.Top,
                       selectedImage.Margin.Right,
                       selectedImage.Margin.Bottom);


                selectedImage.Margin = new Thickness(
                       newSelectedImageMarginValue,
                       selectedImage.Margin.Top,
                       selectedImage.Margin.Right,
                       selectedImage.Margin.Bottom);
            }

        }

        //blah ablha

        //// If we have already clicked on an image...
        //if (imagePressed)
        //{
        //    // Get the pointer position 
        //    Point mousePosition = e.GetPosition(rightClickSlider);

        //    // Calculate the distance the pointer has moved from the original pointer position
        //    var distance = mousePosition.X - pointerPressedPosition.X;

        //    // Calculate the new left margin for the image
        //    var newLeftMarginValue = pointerPressedPosition.X + distance;

        //    // If the new left margin is greater than the left margin maximum minus
        //    // the width of the image, then set the new left margin to the max left margin
        //    if (newLeftMarginValue > rightClickArrowLeftMarginMax - selectedImage.Width)
        //        newLeftMarginValue = rightClickSlider.Width - selectedImage.Width;

        //    // If the new left margin is less than the minimum left margin then set the
        //    // new left margin to the minimum left margin
        //    if (newLeftMarginValue < rightClickArrowLeftMarginMin) newLeftMarginValue = rightClickArrowLeftMarginMin;

        //    // If the left arrow is the one sliding and it runs into the right arrow,
        //    //    move the right arrow also
        //    if (selectedImage.Name == "rightClickLeftArrow"
        //        && newLeftMarginValue > rightClickRightArrow.Margin.Left - selectedImage.Width
        //        && !arrowsShouldCross)
        //    {
        //        var leftMargin = newLeftMarginValue;

        //        //if (leftMargin + rightClickLeftArrow.Width > rightClickArrowLeftMarginMax)
        //        //    leftMargin = rightClickArrowLeftMarginMax;

        //        rightClickRightArrow.Margin =
        //            new Thickness(
        //                leftMargin + selectedImage.Width,
        //                selectedImage.Margin.Top,
        //                selectedImage.Margin.Right,
        //                selectedImage.Margin.Bottom);
        //    }

        //    // If the right arrow is the one sliding and it runs into the left arrow,
        //    //    move the left arrow also
        //    if (selectedImage.Name == "rightClickRightArrow"
        //        && newLeftMarginValue - rightClickRightArrow.Width < rightClickLeftArrow.Margin.Left
        //        && !arrowsShouldCross)
        //    {
        //        rightClickLeftArrow.Margin =
        //            new Thickness(
        //                newLeftMarginValue - rightClickLeftArrow.Width,
        //                selectedImage.Margin.Top,
        //                selectedImage.Margin.Right,
        //                selectedImage.Margin.Bottom);
        //    }

        //    // Update the left margin of the image with the new left margin value
        //    selectedImage.Margin = new Thickness(
        //        newLeftMarginValue,
        //        selectedImage.Margin.Top,
        //        selectedImage.Margin.Right,
        //        selectedImage.Margin.Bottom);

        //}
    }

    #endregion EndRegion - Private Methods



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

