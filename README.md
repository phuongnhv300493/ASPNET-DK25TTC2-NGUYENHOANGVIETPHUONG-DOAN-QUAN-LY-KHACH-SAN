# 🏨 Hệ Thống Quản Lý Khách Sạn - QLKhachSan

Đồ án tốt nghiệp xây dựng website **quản lý đặt phòng khách sạn** trên nền tảng **ASP.NET Core MVC 8.0** với cơ sở dữ liệu **SQL Server**. Hệ thống hỗ trợ 2 phân vùng chính: **Customer** (khách hàng xem phòng) và **Admin** (quản lý phòng, dịch vụ, đặt phòng) cùng phân hệ **Identity** (đăng ký / đăng nhập) tích hợp sẵn.

---

## 📑 Mục lục

1. [Giới thiệu dự án](#1-giới-thiệu-dự-án)
2. [Công nghệ sử dụng](#2-công-nghệ-sử-dụng)
3. [Cấu trúc dự án](#3-cấu-trúc-dự-án)
4. [Cài đặt môi trường](#4-cài-đặt-môi-trường)
5. [Hướng dẫn cài đặt dự án](#5-hướng-dẫn-cài-đặt-dự-án)
6. [Tài khoản mặc định](#6-tài-khoản-mặc-định)
7. [Các đường link quan trọng](#7-các-đường-link-quan-trọng)
8. [Sơ đồ cơ sở dữ liệu (Data Map)](#8-sơ-đồ-cơ-sở-dữ-liệu-data-map)
9. [Các chức năng hiện có](#9-các-chức-năng-hiện-có)
10. [Hướng phát triển thêm](#10-hướng-phát-triển-thêm)
11. [Lỗi thường gặp và cách khắc phục](#11-lỗi-thường-gặp-và-cách-khắc-phục)

---

## 1. Giới thiệu dự án

**QLKhachSan** là hệ thống quản lý đặt phòng khách sạn trực tuyến, cho phép:

- 🛏️ Khách hàng xem danh sách phòng, chi tiết phòng, đặt phòng
- 👨‍💼 Nhân viên lễ tân tiếp nhận đặt phòng, check-in, check-out, quản lý dịch vụ đi kèm
- 🔑 Quản trị viên quản lý toàn bộ hệ thống (phòng, loại phòng, dịch vụ, chương trình khuyến mãi, trạng thái, tài khoản)

### Phân quyền (3 Roles)
| Role | Mô tả | Quyền truy cập |
|------|-------|----------------|
| **Super Admin** | Quản trị cấp cao | Toàn quyền (xem menu quản lý dropdown) |
| **Admins** | Nhân viên lễ tân | Đặt phòng, nhận phòng |
| **Customer** | Khách hàng | Xem phòng, đặt phòng |

---

## 2. Công nghệ sử dụng

| Thành phần | Công nghệ |
|------------|-----------|
| **Ngôn ngữ** | C#, HTML, CSS, JavaScript, Razor |
| **Framework** | ASP.NET Core MVC 8.0 |
| **ORM** | Entity Framework Core 8.0 |
| **Database** | Microsoft SQL Server (Express) |
| **Authentication** | ASP.NET Core Identity |
| **UI Framework** | Bootstrap 4, jQuery, Syncfusion EJ2 |
| **Xuất báo cáo** | GemBox.Document, Syncfusion.DocIO |
| **Quản lý package** | NuGet |

### Packages chính (`datphongkhachsan.csproj`)
```xml
<PackageReference Include="Microsoft.AspNetCore.Identity.EntityFrameworkCore" Version="8.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="8.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="8.0.0" />
<PackageReference Include="Syncfusion.EJ2.AspNet.Core" Version="25.1.37" />
<PackageReference Include="Syncfusion.DocIO.Net.Core" Version="25.1.37" />
<PackageReference Include="GemBox.Document" Version="31.0.0.1078" />
```

---

## 3. Cấu trúc dự án

```
QLKhachSan_DoAn1/
├── src/
│   └── datphongkhachsan/                    # Project ASP.NET Core chính
│       ├── Areas/                            # Phân vùng chức năng
│       │   ├── Admin/                        # Khu vực quản trị
│       │   │   ├── Controllers/              # 11 controllers (DatPhongs, Phongs, DichVus, ...)
│       │   │   ├── Models/                   # ViewModels cho Admin
│       │   │   ├── Views/                    # 41 file .cshtml
│       │   │   └── Data/                     # (đã remove khỏi compile)
│       │   ├── Customer/                     # Khu vực khách hàng (mặc định)
│       │   │   ├── Controllers/              # HomeController, PhongCustomerController
│       │   │   └── Views/                    # Landing page, danh sách phòng
│       │   └── Identity/                     # Hệ thống đăng nhập/đăng ký
│       │       └── Pages/Account/            # Razor Pages cho auth (Login, Register, Manage)
│       ├── Data/
│       │   ├── ApplicationDbContext.cs       # DbContext + seed data
│       │   ├── DbInitializer.cs              # Khởi tạo role + user mặc định
│       │   ├── DataModel/                    # 11 entity classes + 2 ViewModels
│       │   └── Migrations/                   # EF Core migrations
│       ├── Extensions/                       # IEnumerableExtensions, ReflectionExtension, SessionExtensions
│       ├── Models/                           # ErrorViewModel
│       ├── Service/                          # Business logic layer (9 services)
│       ├── Utility/
│       │   └── SD.cs                         # Hằng số (roles, image folder)
│       ├── Views/
│       │   └── Shared/                       # _Layout.cshtml, _LoginPartial, _CookieConsentPartial, Error
│       ├── wwwroot/                          # Tài nguyên tĩnh (css, js, images, lib)
│       ├── Properties/launchSettings.json    # Cấu hình URL debug
│       ├── appsettings.json                  # Connection string
│       ├── Startup.cs                        # Cấu hình services + middleware
│       ├── Program.cs                        # Entry point
│       └── datphongkhachsan.csproj           # File project
├── scripts/                                  # SQL scripts tiện ích
│   ├── delete_locked_admin.sql              # Xóa tài khoản admin bị khóa
│   └── reset_admin.sql                       # Reset admin + role SuperAdmin
├── thesis/
│   └── BáoCáo.docx                          # File báo cáo đồ án
├── Progress_Report/                          # Báo cáo tiến độ (để trống)
├── Script.sql                                # Script SQL tạo database mẫu
└── README.md                                 # File này
```

---

## 4. Cài đặt môi trường

### Yêu cầu phần mềm

| Phần mềm | Phiên bản tối thiểu | Ghi chú |
|----------|---------------------|---------|
| **Visual Studio** | 2022 (17.8+) | Cài workload **ASP.NET and web development** + **.NET 8.0** |
| **.NET SDK** | 8.0 | [Tải tại đây](https://dotnet.microsoft.com/download/dotnet/8.0) |
| **SQL Server** | 2019 / 2022 hoặc **SQL Server Express** | [SSMS tải tại đây](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms) |
| **SQL Server Management Studio (SSMS)** | 19.x trở lên | Dùng để quản lý database |

### Cài đặt Visual Studio 2022
1. Tải Visual Studio Installer từ [visualstudio.microsoft.com](https://visualstudio.microsoft.com/downloads/)
2. Chọn workload **ASP.NET and web development**
3. Tick thêm **.NET 8.0 SDK** trong mục Individual Components
4. Bấm Install

### Cài đặt SQL Server Express
1. Tải SQL Server Express từ [microsoft.com/sql-server/sql-server-downloads](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
2. Trong khi cài, đặt Instance name là `SQLEXPRESS` (mặc định, cũng là tên trong connection string)
3. Cài SSMS riêng

---

## 5. Hướng dẫn cài đặt dự án

### Bước 1 — Clone/Mở project
```bash
git clone <repo-url>
```
Hoặc mở Visual Studio → **File → Open → Project/Solution** → chọn file `src/datphongkhachsan/datphongkhachsan.sln`.

### Bước 2 — Tạo database bằng SSMS

**Cách A — Dùng script SQL có sẵn (khuyến nghị):**

1. Mở **SQL Server Management Studio (SSMS)**
2. Kết nối tới server `THWAVE\SQLEXPRESS` (hoặc `.\\SQLEXPRESS` hoặc `localhost\SQLEXPRESS` tuỳ máy)
3. Click phải vào **Databases → New Query**
4. Mở file `Script.sql` ở thư mục gốc dự án, copy toàn bộ nội dung, paste vào cửa sổ Query
5. Bấm **Execute (F5)** — script sẽ tạo database `QLKhachSan` với các bảng và dữ liệu mẫu

**Cách B — Để EF Core tự tạo (đơn giản hơn):**
- Bỏ qua bước này, EF Core sẽ tự tạo database khi chạy app lần đầu.

### Bước 3 — Cấu hình Connection String

Mở file `src/datphongkhachsan/appsettings.json`, chỉnh lại `DefaultConnection` cho phù hợp máy bạn:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=THWAVE\\SQLEXPRESS;Database=QLKhachSan;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
}
```

**Các biến thể tên server thường gặp:**

| Cách viết | Khi nào dùng |
|-----------|--------------|
| `Server=.\SQLEXPRESS` | SQL Server Express mặc định |
| `Server=THWAVE\SQLEXPRESS` | Có tên máy là `THWAVE` |
| `Server=localhost\SQLEXPRESS` | Dùng localhost |
| `Server=(localdb)\\MSSQLLocalDB` | LocalDB (cài kèm Visual Studio) |
| `Server=.` | SQL Server mặc định không có instance |
| `Server=192.168.1.10,1433` | Kết nối từ xa |

> ⚠️ Nếu bạn không dùng Windows Authentication, đổi sang dạng:
> ```json
> "Server=.\SQLEXPRESS;Database=QLKhachSan;User Id=sa;Password=YOUR_PASSWORD;TrustServerCertificate=True"
> ```

### Bước 4 — Restore NuGet Packages

Trong Visual Studio:
- Click phải vào **Solution → Restore NuGet Packages**
- Hoặc mở **Package Manager Console** gõ: `Update-Package -reinstall`

### Bước 5 — Chạy ứng dụng

1. Đặt `datphongkhachsan` làm **Startup Project** (chuột phải project → Set as Startup Project)
2. Bấm **F5** hoặc **IIS Express** / **datphongkhachsan** ở thanh debug
3. Trình duyệt tự mở:
   - URL mặc định: **http://localhost:5000** hoặc **https://localhost:5001**
   - URL IIS Express: **http://localhost:54552**
4. Lần đầu chạy, `DbInitializer` sẽ tự tạo **3 roles** + **2 user mặc định**

### Bước 6 — Tạo Migration mới (nếu cần)

Nếu sửa model và muốn cập nhật database:
```powershell
# Package Manager Console
Add-Migration TenMigration
Update-Database
```

Hoặc dùng CLI:
```bash
dotnet ef migrations add TenMigration
dotnet ef database update
```

---

## 6. Tài khoản mặc định

Sau khi chạy lần đầu, hệ thống tự động tạo các tài khoản sau (xem trong `Data/DbInitializer.cs`):

| Email | Mật khẩu | Tên hiển thị | Role | Quyền |
|-------|----------|--------------|------|-------|
| **admin@gmail.com** | `admin123` | Quản Trị Viên | `Super Admin` | Toàn quyền (xem được menu dropdown Admin) |
| **user1@gmail.com** | `user123` | Khách Hàng 1 | `Customer` | Xem phòng, đặt phòng |

### ⚠️ Lưu ý về bảo mật

- Mật khẩu ở trên là **mặc định cho đồ án**, **phải đổi trước khi deploy thật**
- Chính sách mật khẩu đã được nới lỏng trong `Startup.cs` (chỉ cần 1 ký tự, không yêu cầu số/hoa/thường/đặc biệt) — đây là **mặc định cho môi trường dev**, nên siết lại khi lên production

### Tạo tài khoản nhân viên mới

Vào **Identity → Register** (đăng ký) trên web, sau đó vào **SSMS** cập nhật role:

```sql
-- Gán role "Admins" cho user mới
INSERT INTO AspNetUserRoles (UserId, RoleId)
SELECT u.Id, r.Id
FROM AspNetUsers u, AspNetRoles r
WHERE u.Email = 'nhanvien@gmail.com' AND r.Name = 'Admins';

-- Hoặc gán "Super Admin"
INSERT INTO AspNetUserRoles (UserId, RoleId)
SELECT u.Id, r.Id
FROM AspNetUsers u, AspNetRoles r
WHERE u.Email = 'quantri@gmail.com' AND r.Name = 'Super Admin';
```

### Script tiện ích trong `scripts/`
- `delete_locked_admin.sql` — Xóa user admin bị khóa để reset bằng cách sửa code và chạy lại `DbInitializer`
- `reset_admin.sql` — Xóa user cũ + role SuperAdmin, sau đó vào app để tạo lại

---

## 7. Các đường link quan trọng

### URL khi chạy dev

| URL | Mô tả |
|-----|-------|
| `http://localhost:5000` | Trang chủ Customer (landing page) |
| `https://localhost:5001` | Trang chủ Customer (HTTPS) |
| `http://localhost:54552` | URL qua IIS Express |
| `https://localhost:44340` | URL qua IIS Express (HTTPS) |

### Các route chính

| Đường dẫn | Quyền | Mô tả |
|-----------|-------|-------|
| `/` hoặc `/Customer/Home/Index` | Public | Trang chủ |
| `/Customer/PhongCustomer/IndexRoom` | Public | Danh sách phòng cho khách |
| `/Customer/PhongCustomer/DetailsRoom/{id}` | Public | Chi tiết 1 phòng |
| `/Identity/Account/Login` | Public | Đăng nhập |
| `/Identity/Account/Register` | Public | Đăng ký |
| `/Identity/Account/Logout` | Đã đăng nhập | Đăng xuất |
| `/Identity/Account/Manage/Index` | Đã đăng nhập | Quản lý tài khoản cá nhân |
| `/Admin/DatPhongs/Index` | Admin / Super Admin | Danh sách phiếu đặt phòng |
| `/Admin/DatPhongCart/Index` | Admin / Super Admin | Tạo phiếu đặt phòng (cart) |
| `/Admin/Phongs/Index` | Super Admin | Quản lý phòng |
| `/Admin/LoaiPhongs/Index` | Super Admin | Quản lý loại phòng |
| `/Admin/DichVus/Index` | Super Admin | Quản lý dịch vụ |
| `/Admin/LoaiDichVus/Index` | Super Admin | Quản lý loại dịch vụ |
| `/Admin/ChuongTrinhs/Index` | Super Admin | Quản lý chương trình khuyến mãi |
| `/Admin/TrangThais/Index` | Super Admin | Quản lý trạng thái |
| `/Admin/Accounts/Index` | Super Admin | Quản lý tài khoản nhân viên |
| `/Admin/GetListDatPhongs/Index` | Admin / Super Admin | Tìm phòng trống theo ngày |
| `/Admin/GetListDichVu/Index` | Admin / Super Admin | Chọn dịch vụ cho phiếu đặt |

---

## 8. Sơ đồ cơ sở dữ liệu (Data Map)

### Các bảng chính

```
┌──────────────────────┐       ┌────────────────────────��─┐
│ LoaiPhong            │       │ Phong                    │
│──────────────────────│       │──────────────────────────│
│ Id (PK)              │◄──┐   │ Id (PK)                  │
│ Name                 │   │   │ Name                     │
│ ShortDescription     │   └───│ LoaiPhongId (FK)         │
│ HinhUrl              │       │ ShortDescription         │
└──────────────────────┘       │ Price (Money)            │
                               │ HinhUrl                  │
                               │ ChuongTrinhId (FK?)      │
                               └──────────┬───────────────┘
                                          │
                                          │
┌──────────────────────┐                 │
│ ChuongTrinh          │                 │
│──────────────────────│                 │
│ Id (PK)              │◄────────────────┘
│ TenChuongTrinh       │
│ TiLeThayDoiGia       │
│ IsTang (bool)        │
└──────────────────────┘

┌──────────────────────┐       ┌──────────────────────────┐
│ LoaiDichVu           │       │ DichVu                   │
│──────────────────────│       │──────────────────────────│
│ Id (PK)              │◄──┐   │ Id (PK)                  │
│ Name                 │   │   │ Name                     │
│ Description          │   └───│ LoaiDvid (FK)            │
└──────────────────────┘       │ ShortDescription         │
                               │ Price                    │
                               │ ImageUrl                 │
                               │ InStock                  │
                               │ SoLuongMua (NotMapped)   │
                               └──────────────────────────┘

┌──────────────────────┐       ┌──────────────────────────┐
│ AccountSys           │       │ DatPhong                 │
│──────────────────────│       │──────────────────────────│
│ Id (PK, IdentityUser)│◄──┐   │ Id (PK)                  │
│ Name                 │   │   │ TenNguoiDat              │
│ IsSuperAdmin         │   └───│ Address                  │
│ (extends IdentityUser)│      │ Cmnd                     │
└──────────────────────┘       │ Sdt                      │
                               │ TienDatCoc (Money)       │
                               │ ThoiGianNhanPhongDuKien  │
                               │ ThoiGianTraPhongDuKien   │
                               │ TongTien (Money)         │
                               │ AccoutId (FK)            │
                               └──────────┬───────────────┘
                                          │
                ┌─────────────────────────┼──────────────────────────┐
                │                                                    │
                ▼                                                    ▼
┌──────────────────────────┐              ┌──────────────────────────┐
│ ChiTietDatPhong          │              │ ChiTietDichVuDatPhong    │
│──────────────────────────│              │──────────────────────────│
│ DatPhongId (PK, FK)      │              │ Id (PK)                  │
│ PhongId (PK, FK)         │              │ DatPhongId (FK)          │
│ TrangThaiId (PK, FK)     │              │ DichVuId (FK)            │
│ ThoiGian (PK)            │              │ SoLuong                  │
└──────────┬───────────────┘              └──────────────────────────┘
           │
           ▼
┌──────────────────────┐
│ TrangThai            │
│──────────────────────│
│ Id (PK)              │
│ Name                 │ ← Seed: "Chưa Nhận", "Đã Nhận", "Đã Thanh Toán"
└──────────────────────┘
```

### Bảng Identity (do ASP.NET Core Identity tạo)

```
AspNetUsers           ← Tài khoản người dùng
AspNetRoles           ← Roles: Admins, Super Admin, Customer
AspNetUserRoles       ← User ↔ Role (many-to-many)
AspNetUserClaims
AspNetUserLogins
AspNetUserTokens
AspNetRoleClaims
```

### Quan hệ (Relationships)

| Quan hệ | Loại | Mô tả |
|---------|------|-------|
| `Phong → LoaiPhong` | N-1 | Nhiều phòng thuộc 1 loại |
| `Phong → ChuongTrinh` | N-1 (nullable) | Phòng có thể thuộc 1 chương trình khuyến mãi |
| `DichVu → LoaiDichVu` | N-1 | Nhiều dịch vụ thuộc 1 loại |
| `DatPhong → AccountSys` | N-1 | Phiếu đặt phòng gắn với tài khoản |
| `ChiTietDatPhong → DatPhong, Phong, TrangThai` | N-N-N | Chi tiết từng phòng theo từng ngày trong phiếu đặt |
| `ChiTietDichVuDatPhong → DatPhong, DichVu` | N-N | Chi tiết dịch vụ trong phiếu đặt |

### Dữ liệu mẫu (Seed trong `ApplicationDbContext.cs`)

| Bảng | Seed data |
|------|-----------|
| **LoaiPhong** | Phòng Đơn, Phòng Đôi |
| **Phong** | A101, A102, A103, A104, A105, A106, A107, A108 |
| **LoaiDichVu** | Thức Ăn, Nước Uống |
| **DichVu** | Nước Tăng Lực, Nước Suối |
| **TrangThai** | Chưa Nhận, Đã Nhận, Đã Thanh Toán |
| **DatPhong** | 3 phiếu đặt (Nguyen Phuoc, Nguyen Truc, Phan Tuyen) |
| **AccountSys** | admin@gmail.com, user1@gmail.com |

---

## 9. Các chức năng hiện có

### 9.1. Phân vùng Customer (Khách hàng — public)

| Chức năng | Mô tả |
|-----------|-------|
| **Xem trang chủ** | Landing page với carousel phòng, giới thiệu khách sạn, dịch vụ |
| **Xem danh sách phòng** | Grid card hiển thị các phòng kèm giá, loại phòng |
| **Xem chi tiết phòng** | Tên, mô tả, giá, loại phòng, hình ảnh, chương trình KM |
| **Đăng ký tài khoản** | Form đăng ký (tên, SĐT, email, mật khẩu) |
| **Đăng nhập / Đăng xuất** | Sử dụng ASP.NET Identity |
| **Quản lý tài khoản** | Cập nhật SĐT, đổi email, đổi mật khẩu, bật/tắt 2FA |

### 9.2. Phân vùng Admin (Quản trị — cần role)

| Module | Chức năng |
|--------|-----------|
| **Quản lý Phiếu đặt phòng** | Tạo mới, xem danh sách, tìm kiếm theo CMND/ngày nhận, sắp xếp, xem chi tiết, nhận phòng, xóa |
| **Giỏ đặt phòng (Cart)** | Chọn nhiều phòng + dịch vụ vào phiếu, xác nhận đặt |
| **Tìm phòng trống** | Lọc phòng theo khoảng ngày (ngày đến - ngày đi) |
| **Quản lý Phòng** | CRUD phòng, upload hình ảnh, gán loại phòng, chương trình |
| **Quản lý Loại phòng** | CRUD loại phòng (Phòng Đơn, Phòng Đôi, ...) |
| **Quản lý Dịch vụ** | CRUD dịch vụ (Nước, Đồ ăn, ...), gán loại |
| **Quản lý Loại dịch vụ** | CRUD loại dịch vụ |
| **Quản lý Chương trình** | CRUD chương trình khuyến mãi (tăng/giảm giá theo %) |
| **Quản lý Trạng thái** | CRUD trạng thái phiếu đặt |
| **Quản lý Tài khoản** | Vô hiệu hóa / xóa tài khoản nhân viên, super admin |

### 9.3. Tính năng kỹ thuật đã có

- ✅ ASP.NET Identity với 3 roles + phân quyền theo role
- ✅ Session-based shopping cart (key: `ssPhongCart`, `ssDichVuCart`)
- ✅ Auto migration khi chạy app (`dbInitializer.Initialize()`)
- ✅ Auto seed data khi DB trống
- ✅ Hỗ trợ upload hình ảnh phòng (`Phongs/Create`, `Phongs/Edit`)
- ✅ Tìm kiếm + sắp xếp trong trang danh sách phiếu đặt
- ✅ Responsive UI với Bootstrap 4
- ✅ Cookie consent banner
- ✅ Xác thực 2 yếu tố (2FA) với Authenticator app

---

## 10. Hướng phát triển thêm

### 10.1. Cải tiến ngắn hạn (1–2 tuần)

- [ ] **Upload nhiều hình ảnh cho 1 phòng** — Hiện tại bảng `HinhPhong` đã có nhưng chưa dùng
- [ ] **Tính tổng tiền tự động** khi tạo phiếu đặt (đang để nhập tay)
- [ ] **Tính giá theo chương trình khuyến mãi** (bảng `ChuongTrinh` có `IsTang`, `TiLeThayDoiGia` nhưng chưa áp dụng)
- [ ] **Lịch sử đặt phòng** cho khách hàng (xem lại các phiếu đã đặt)
- [ ] **Tìm kiếm + phân trang** cho danh sách phòng, dịch vụ
- [ ] **Validation form chặt hơn** (CMND phải 9-12 số, SĐT đúng định dạng VN, email đúng format)
- [ ] **Đổi mật khẩu lần đầu** bắt buộc cho user mới

### 10.2. Tính năng trung hạn (1–2 tháng)

- [ ] **Thanh toán online** tích hợp VNPay, Momo, ZaloPay
- [ ] **Gửi email xác nhận** đặt phòng (đã có `EmailSender` interface, cần cấu hình SMTP thật)
- [ ] **Hóa đơn PDF / DOCX** xuất bằng GemBox.Document
- [ ] **Báo cáo thống kê** doanh thu theo tháng/quý/năm (dùng Syncfusion EJ2 Chart)
- [ ] **Dashboard** cho admin với các chỉ số (số phòng trống, đang thuê, doanh thu hôm nay)
- [ ] **Quản lý nhân viên** chi tiết hơn (chức vụ, lương, ca làm)
- [ ] **Đánh giá / review** c��a khách sau khi trả phòng

### 10.3. Tính năng dài hạn

- [ ] **Đặt phòng online cho khách vãng lai** (chưa cần đăng ký)
- [ ] **Quản lý nhiều chi nhánh** khách sạn
- [ ] **Tích hợp API bên thứ 3**: Booking.com, Traveloka, Agoda
- [ ] **App mobile** (React Native / Flutter) gọi API từ backend này
- [ ] **Chuyển sang REST API + SPA** (ReactJS / VueJS frontend, .NET làm backend)
- [ ] **Hệ thống điểm thưởng** cho khách hàng thân thiết
- [ ] **Chatbot hỗ trợ** đặt phòng

### 10.4. Cải tiến kỹ thuật

- [ ] Tách Service Layer ra project riêng, dùng **Repository Pattern + Unit of Work**
- [ ] Thêm **unit test** (xUnit) + **integration test**
- [ ] Chuyển sang **Clean Architecture** (Domain, Application, Infrastructure, Web)
- [ ] Dùng **MediatR** + **CQRS** cho các command/query
- [ ] Thêm **logging** với Serilog, gửi lên Seq / ELK
- [ ] **AutoMapper** để map Entity ↔ DTO
- [ ] **FluentValidation** thay cho DataAnnotations
- [ ] **Docker** hoá + docker-compose (SQL Server + Web)
- [ ] CI/CD với **GitHub Actions**
- [ ] Deploy lên **Azure** / **AWS** / **VPS**
- [ ] Đổi connection string sang **User Secrets** thay vì hard-code

---

## 11. Lỗi thường gặp và cách khắc phục

### ❌ Lỗi 1: Không kết nối được SQL Server

**Triệu chứng:**
```
A network-related or instance-specific error occurred while establishing a connection to SQL Server.
The server was not found or was not accessible.
```

**Nguyên nhân:** Sai tên server trong connection string, hoặc SQL Server chưa chạy.

**Cách fix:**
1. Mở **SQL Server Configuration Manager** → kiểm tra service **SQL Server (SQLEXPRESS)** đang chạy
2. Trong SSMS, kết nối thử bằng tên server xuất hiện ở màn hình Connect
3. Sửa lại `appsettings.json`:
   ```json
   "Server=.\\SQLEXPRESS"  hoặc  "Server=localhost\\SQLEXPRESS"
   ```
4. Nếu vẫn lỗi, mở **Services** (services.msc) → start service `SQL Server (SQLEXPRESS)`

---

### ❌ Lỗi 2: Login failed for user

**Triệu chứng:**
```
Cannot open database "QLKhachSan" requested by the login. The login failed.
```

**Nguyên nhân:** Dùng SQL Authentication nhưng sai user/pass, hoặc database chưa được tạo.

**Cách fix:**
- Nếu dùng Windows Authentication (mặc định), đảm bảo connection string có `Trusted_Connection=True`
- Nếu dùng SQL Auth:
  ```json
  "Server=.\\SQLEXPRESS;Database=QLKhachSan;User Id=sa;Password=MatKhauCuaBan;TrustServerCertificate=True"
  ```
- Nếu database chưa tồn tại: chạy `Script.sql` hoặc xóa DB cũ rồi để EF tự tạo

---

### ❌ Lỗi 3: Build failed — Missing NuGet packages

**Triệu chứng:**
```
The type or namespace name 'X' could not be found
```

**Cách fix:**
- Visual Studio: Right-click Solution → **Restore NuGet Packages**
- Hoặc PMC: `Update-Package -reinstall`
- Hoặc CLI: `dotnet restore`

---

### ❌ Lỗi 4: Migration conflict / Pending migrations

**Triệu chứng:**
```
There are pending model changes
```
hoặc
```
The model backing the 'ApplicationDbContext' context has changed since the database was created
```

**Cách fix:**
```powershell
# Xóa migration cũ (nếu chưa apply lên DB production)
Remove-Migration

# Tạo migration mới
Add-Migration TenMigrationMoi

# Apply vào DB
Update-Database
```

Hoặc hard reset database (XÓA HẾT DỮ LIỆU):
```sql
USE master;
DROP DATABASE QLKhachSan;
```
Rồi chạy lại app, EF sẽ tự tạo DB mới + seed data.

---

### ❌ Lỗi 5: Port 5000 / 5001 / 54552 đã bị chiếm

**Triệu chứng:**
```
Failed to bind to address http://localhost:5000: address already in use
```

**Cách fix:**
- Đổi port trong `Properties/launchSettings.json`:
  ```json
  "applicationUrl": "https://localhost:7001;http://localhost:7000"
  ```
- Hoặc tắt ứng dụng đang chiếm port (xem Task Manager → Details tab → sort theo PID, hoặc dùng `netstat -ano | findstr :5000` để tìm)

---

### ❌ Lỗi 6: Tài khoản admin bị khóa (LockoutEnd) do nhập sai pass nhiều lần

**Triệu chứng:** Đăng nhập với `admin@gmail.com / admin123` báo "Locked out".

**Cách fix:**

**Cách A — Dùng script có sẵn:**
Mở SSMS → chạy file `scripts/delete_locked_admin.sql` → sau đó đăng nhập lại (DbInitializer sẽ tự tạo lại).

**Cách B — Unlock trong SSMS:**
```sql
UPDATE AspNetUsers
SET LockoutEnd = NULL,
    AccessFailedCount = 0
WHERE Email = 'admin@gmail.com';
```

**Cách C — Reset toàn bộ admin:**
1. Sửa `Data/DbInitializer.cs` → thêm dòng `EmailConfirmed = true` nếu chưa có
2. Chạy lại app → user sẽ được tạo lại với pass mới (do logic `FindByEmailAsync(...).GetAwaiter().GetResult() == null` chỉ chạy khi user chưa tồn tại — cần xóa trước)

---

### ❌ Lỗi 7: View không hiển thị đúng layout / bị trắng trang

**Triệu chứng:** Trang trắng, hoặc CSS không load.

**Cách fix:**
- Bấm **F12** trong trình duyệt → tab **Console** xem lỗi
- Thường do:
  1. **Static files không serve** → kiểm tra `app.UseStaticFiles()` trong `Startup.Configure`
  2. **Đường dẫn `~/css/style.css` không resolve** → kiểm tra `wwwroot/css/style.css` tồn tại
  3. **Lỗi Razor compilation** → xem log trong cửa sổ Output của VS

---

### ❌ Lỗi 8: "Cannot insert duplicate key" khi thêm phòng/dịch vụ

**Triệu chứng:** Insert bị lỗi trùng khóa chính.

**Cách fix:**
- ID là `int` tự tăng, kiểm tra column có `IDENTITY(1,1)` trong DB chưa
- Chạy lại `Script.sql` nếu cấu trúc DB bị lệch

---

### ❌ Lỗi 9: Session timeout (giỏ hàng bị mất)

**Triệu chứng:** Thêm phòng vào giỏ xong, F5 thì mất.

**Cách fix:**
- Session timeout đang set **10 phút** trong `Startup.cs`. Có thể tăng:
  ```csharp
  options.IdleTimeout = TimeSpan.FromMinutes(60);
  ```
- Hoặc lưu cart vào DB thay vì session

---

### ❌ Lỗi 10: Compile error "The type 'X' exists in both 'Y.dll' and 'Z.dll'"

**Nguyên nhân:** Conflict giữa 2 phiên bản package (thường gặp với Syncfusion).

**Cách fix:**
1. Mở NuGet Package Manager
2. Tìm package bị conflict → Update lên cùng version
3. Clean solution: `Build → Clean Solution` rồi `Rebuild`

---

### ❌ Lỗi 11: Syncfusion báo "This application uses Syncfusion components but a valid license is required"

**Cách fix:** Syncfusion bản Community miễn phí cho cá nhân/revenue < $1M. Đăng ký license key tại [syncfusion.com](https://www.syncfusion.com/account/community-license) và thêm vào `Program.cs`:
```csharp
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("YOUR_LICENSE_KEY");
```

---

### ❌ Lỗi 12: "Cannot find compilation library" khi publish

**Cách fix:**
- Cài đầy đủ **ASP.NET Core Runtime 8.0** trên server
- Publish bằng Visual Studio: Right-click project → **Publish** → chọn Folder → Publish
- Đảm bảo file `appsettings.json` được copy sang thư mục publish

---

### ❌ Lỗi 13: Razor compile error "The type or namespace name 'IChiTietDichVuDatPhong' could not be found"

**Cách fix:**
- Một số Service interface bị remove khỏi `Startup.cs` (xem comment `//`). Nếu Controller nào inject interface đó sẽ lỗi
- Đăng ký lại trong `Startup.ConfigureServices`:
  ```csharp
  services.AddTransient<IChiTietDichVuDatPhong, ChiTietDichVuDatPhongService>();
  ```

---

### 🔧 Mẹo debug hữu ích

- **Xem log chi tiết:** Trong `appsettings.Development.json` đã có `LogLevel.Default = "Debug"` — log sẽ xuất hiện trong cửa sổ Output
- **Xem SQL query:** Trong `DbContext.OnConfiguring` thêm `optionsBuilder.LogTo(Console.WriteLine)` để in SQL ra console
- **Reset database nhanh:**
  ```powershell
  # Trong Package Manager Console
  Drop-Database
  Update-Database
  ```

---

## 📞 Liên hệ / Đóng góp

Dự án đồ án tốt nghiệp. Mọi câu hỏi vui lòng liên hệ qua email sinh viên thực hiện (xem trong file `thesis/BáoCáo.docx`).

---

## 📜 License

Đồ án học tập — sử dụng cho mục đích tham khảo và học tập.

---

**Chúc bạn cài đặt thành công! 🚀**