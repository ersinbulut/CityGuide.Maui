using CityGuide.Maui.Models;
using CityGuide.Maui.Services;

namespace CityGuide.Maui.Views;

public partial class RegisterPage : ContentPage
{
    private readonly AppDatabase _db = new AppDatabase();
    public RegisterPage()
	{
		InitializeComponent();
	}


    private void OnTogglePasswordVisibility(object sender, TappedEventArgs e)
    {
        // IsPassword'ü tersine çevir
        PasswordEntry.IsPassword = !PasswordEntry.IsPassword;

        // İkonu duruma göre güncelle
        if (PasswordEntry.IsPassword)
        {
            // Şifre gizli -> "göster" ikonu
            PasswordToggleIcon.Text = "🙈";
        }
        else
        {
            // Şifre açık -> "gizle" ikonu
            PasswordToggleIcon.Text = "🐵";
        }
    }


    private async void OnTermsTapped(object sender, TappedEventArgs e)
    {
        string termsText =
              "MİLANO ŞEHİR REHBERİ - ŞARTLAR VE KOŞULLAR\n\n" +
              "1.üyelik \n" +
              "Bu uygulamayı kullanarak sağladığınız bilgilerin doğru ve güncel olduğunu kabul edersiniz. \n\n" +
              "2.Gizlilik \n" +
              "Kişisel bilgilerinizin gizliliğini korumak için gerekli önlemleri alıyoruz. Verileriniz üçüncü taraflarla paylaşılmayacaktır.\n\n" +
              "3.Kullanım Koşulları \n" +
              "Uygulamayı yalnızca yasal amaçlarla kullanmayı kabul edersiniz. Herhangi bir yasa dışı faaliyet veya kötüye kullanım durumunda hesabınız askıya alınabilir veya kapatılabilir.\n\n" +
              "4.Sorumluluk Reddi \n" +
              "Uygulama, sağlanan bilgilerin doğruluğunu garanti etmez. Kullanıcılar, uygulamayı kendi sorumlulukları altında kullanır ve herhangi bir zarar veya kayıp durumunda uygulama geliştiricileri sorumlu tutulamaz.\n\n";
       
        await DisplayAlertAsync("Şartlar ve Koşullar", termsText,"Kapat");
    }



    private async void OnSignInTapped(object sender, TappedEventArgs e)
    {
        await DisplayAlert("Giriş Yap", "Giriş ekranı yakında eklenecek.", "Tamam");
    }

    private async void OnSupportTapped(object sender, TappedEventArgs e)
    {
        await DisplayAlert("Destek", "Destek için: destek@milanorehberi.com", "Tamam");
    }


    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        // 1) Girilen veriyi oku
        string fullName = FullNameEntry.Text;
        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;
        string confirmPassword = ConfirmPasswordEntry.Text;

        // 2) Boş alan kontrolü
        if (string.IsNullOrWhiteSpace(fullName) ||
            string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(password) ||
            string.IsNullOrWhiteSpace(confirmPassword))
        {
            await DisplayAlert("Eksik Bilgi", "Lütfen tüm alanları doldurun.", "Tamam");
            return;
        }

        // 3) Şifreler eşleşiyor mu?
        if (password != confirmPassword)
        {
            await DisplayAlert("Hata", "Şifreler eşleşmiyor.", "Tamam");
            return;
        }

        // 4) Şartlar kabul edildi mi?
        if (!TermsCheckBox.IsChecked)
        {
            await DisplayAlert("Hata", "Şartları kabul etmelisiniz.", "Tamam");
            return;
        }

        // 5) Veritabanına kaydet
        try
        {
            var newUser = new User
            {
                FullName = fullName,
                Email = email,
                Password = password
            };

            await _db.AddUserAsync(newUser);

            await DisplayAlert("Başarılı", $"Hoş geldiniz, {fullName}! Kaydınız oluşturuldu.", "Tamam");
        }
        catch (Exception)
        {
            // [Unique] ihlali: e-posta zaten kayıtlı
            await DisplayAlert("Hata", "Bu e-posta adresi zaten kayıtlı.", "Tamam");
        }
    }


}