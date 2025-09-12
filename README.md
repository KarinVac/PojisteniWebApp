# Správa Pojištění - Webová Aplikace v ASP.NET Core

Webová aplikace pro správu pojištěných osob a jejich pojistných smluv, vytvořená v rámci mého studia v rekvalifikačním kurzu Programátor WWW aplikací v C# .NET.

## 🖼️ Ukázka Aplikace

<p align="center">
<img width="50%" alt="Screenshot_2" src="https://github.com/user-attachments/assets/f71b1c18-fe35-402b-babc-f9d135f7ae6b" />
<img width="45%" alt="Screenshot_3" src="https://github.com/user-attachments/assets/4ae994ce-c859-4af8-828f-644f5bfe0a06" />
<img width="30.5%" alt="Screenshot_7" src="https://github.com/user-attachments/assets/5e177b8e-946b-481c-af1f-f167554b57f4" />
<img width="30%" alt="Screenshot_9" src="https://github.com/user-attachments/assets/118ac719-591a-4aa9-980a-3519085734d7" />
</p>


## O Projektu

Cílem projektu bylo vytvořit plně funkční informační systém pro pojišťovacího makléře. Aplikace umožňuje centrálně spravovat databázi klientů a jejich pojistných smluv. Důraz byl kladen na čistý kód, responzivní design a implementaci uživatelských rolí pro zabezpečení dat.

Databáze se vytvoří a naplní demo daty **automaticky** při prvním spuštění aplikace.

### Klíčové Funkce

- **Kompletní správa klientů** (CRUD - Vytvoření, Čtení, Editace, Smazání).
- **Kompletní správa pojištění** (CRUD) s přímým navázáním na konkrétního klienta.
- **Registrace a přihlašování uživatelů.**
- **Systém uživatelských rolí** (Administrátor vs. Běžný uživatel).
  - **Administrátor** má plný přístup k datům všech klientů a pojištění. Může vše spravovat.
  - **Běžný uživatel** po přihlášení vidí pouze své vlastní údaje a pojištění.
- **Responzivní design** pro pohodlné zobrazení na desktopu i mobilních zařízeních.

### Použité Technologie

- **Backend:** C#, ASP.NET Core MVC, Entity Framework Core
- **Frontend:** HTML, CSS, Bootstrap
- **Databáze:** MS-SQL (prostřednictvím SQL Server LocalDB)
- **Autentizace & Autorizace:** ASP.NET Core Identity
- **Další nástroje:** AutoMapper, Git, Visual Studio

## 🚀 Spuštění Projektu

1. Naklonujte si repozitář: `git clone https://github.com/KarinVac/PojisteniWebApp.git`
2. Otevřete projekt ve Visual Studiu.
3. Spusťte aplikaci stisknutím klávesy **F5**.
4. Při prvním spuštění se automaticky vytvoří databáze a naplní se demo daty níže.

## 🔐  Účty pro vyzkoušení

Pro otestování funkcionality aplikace můžete použít následující předvytvořené účty:

| Role | Email | Heslo | Popis |
| :--- | :--- | :--- | :--- |
| **Administrátor** | `admin@admin.com` | `Heslo.123` | Vidí vše, může vše editovat a mazat. |
| **Uživatel** | `harry@potter.cz` | `Password.123` | Vidí jen svůj profil a svá pojištění. |
| **Uživatel**| `ron@weasley.cz` | `Password.123` | Vidí jen svůj profil a svá pojištění. |
| **Uživatel** | `hermiona@granger.cz`| `Password.123` | Vidí jen svůj profil a svá pojištění. |

Samozřejmě je také možné si registrací vytvořit vlastního uživatele, který automaticky získá roli "Uživatel".

