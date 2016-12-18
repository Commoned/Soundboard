using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using RobbieRotten.Model;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RobbieRotten
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Sound> Sounds;
        private List<String> Suggestions;
        private List<MenuItem> MenuItems;

        public MainPage()
        {
            this.InitializeComponent();
            Sounds = new ObservableCollection<Sound>();
            SoundManager.GetAllSounds(Sounds);

            MenuItems = new List<MenuItem>();
            MenuItems.Add(new MenuItem { IconFile = "Assets/Icons/dank.png", Category = SoundCategory.Dank });
            MenuItems.Add(new MenuItem { IconFile = "Assets/Icons/Games.png", Category = SoundCategory.Games });
            MenuItems.Add(new MenuItem { IconFile = "Assets/Icons/Earrape.png", Category = SoundCategory.Earrape });
            MenuItems.Add(new MenuItem { IconFile = "Assets/Icons/overwatch.png", Category = SoundCategory.Overwatch });
            MenuItems.Add(new MenuItem { IconFile = "Assets/Icons/Sovereign.png", Category = SoundCategory.Sovereign });
            MenuItems.Add(new MenuItem { IconFile = "Assets/Icons/Dwarf.png", Category = SoundCategory.Dwarf });
            MenuItems.Add(new MenuItem { IconFile = "Assets/Icons/Arnold.png", Category = SoundCategory.Arnold });

            BackButton.Visibility = Visibility.Collapsed;
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            SplitView.IsPaneOpen = !SplitView.IsPaneOpen;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            goBack();
        }

        private void Search_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (String.IsNullOrEmpty(sender.Text)) goBack();

            SoundManager.GetAllSounds(Sounds);
            Suggestions = Sounds
                .Where(p => p.Name.StartsWith(sender.Text))
                .Select(p => p.Name)
                .ToList();
            Search.ItemsSource = Suggestions;
        }

        private void goBack()
        {
            SoundManager.GetAllSounds(Sounds);
            CategoryTextBlock.Text = "All Sounds";
            MenuItemsList.SelectedItem = null;
            BackButton.Visibility = Visibility.Collapsed;
        }

        private void Search_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            SoundManager.GetSoundsByName(Sounds, sender.Text);
            CategoryTextBlock.Text = sender.Text;
            MenuItemsList.SelectedItem = null;
            BackButton.Visibility = Visibility.Visible;
        }

        private void MenuItemsList_ItemClick(object sender, ItemClickEventArgs e)
        {
            var menuItem = (MenuItem)e.ClickedItem;

            CategoryTextBlock.Text = menuItem.Category.ToString();
            SoundManager.GetSoundsByCategory(Sounds, menuItem.Category);
            BackButton.Visibility = Visibility.Visible;

        }

        private async void SoundGridView_Drop(object sender, DragEventArgs e)
        {
            if (e.DataView.Contains(StandardDataFormats.StorageItems))
            {
                var items = await e.DataView.GetStorageItemsAsync();

                if (items.Any())
                {
                    var storageFile = items[0] as StorageFile;
                    var contentType = storageFile.ContentType;

                    StorageFolder folder = ApplicationData.Current.LocalFolder;

                    if (contentType == "audio/wav" || contentType == "audio/mpeg")
                    {
                        StorageFile newFile =
                            await storageFile.CopyAsync(
                                folder,
                                storageFile.Name,
                                NameCollisionOption.GenerateUniqueName);

                        MyMediaElement.SetSource(
                            await storageFile.OpenAsync(FileAccessMode.Read),
                            contentType);

                        MyMediaElement.Play();
                    }
                }
            }
        }

        private void SoundGridView_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;

            e.DragUIOverride.Caption = "drop to create a custom sound and tile";
            e.DragUIOverride.IsCaptionVisible = true;
            e.DragUIOverride.IsContentVisible = true;
            e.DragUIOverride.IsGlyphVisible = true;
        }   

        private void SoundGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var sound = (Sound)e.ClickedItem;
            var vol = MyMediaElement.Volume;
            if (sound.Category == SoundCategory.Earrape)
                MyMediaElement.Volume = 0.2f;
            else
                MyMediaElement.Volume = 0.5f;
            
            MyMediaElement.Source = new Uri(this.BaseUri, sound.AudioFile);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MyMediaElement.Source = null;
        }
    }
}
