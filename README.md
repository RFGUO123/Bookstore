# 基拉網路書局

## 小型購物商城網站作品
## 作者 : RFGUO

### 作品網站  [連結](http://ec2-54-65-85-160.ap-northeast-1.compute.amazonaws.com:5290/)
### 特色介紹
- 透過Ajax實作購物車，留言板和帳號驗證功能
- 採用 Controller - Service - Repository 架構，使專案較易於維護
- 在資料表重點欄位加上索引 (Index)，加快 SQL 語句查詢速度
### 運行環境要求
- .Net framework >=  4.8
- SQL server >= 2019
- LocalDB >= 2019

### 安裝專案步驟
- 利用Visual Studio發佈到指定資料夾
- 設定IIS對準發布資料夾路徑

### 專案目錄架構
- 主要目錄

```
/Controller - 擺放 Controller 檔案
/Model - 擺放 Entity Framework 資料庫設定檔
/views - 視圖模板檔案
/Service - 擺放 Serivce 業務邏輯的檔案
/Repository - 擺放 Repository 操作資料庫的檔案
/Util - 擺放 static 通用的輔助方法
/Web.config - 設定檔
```
- 其他重要目錄檔案

```
/Images - 擺放 Product 圖片的檔案
/JavaScript - 擺放專案通用 JS 的檔案
/Styles - 擺放視圖模板通用 CSS 的檔案
```

### 資料表 table 介紹
- Member : 使用者
- ProductType : 商品種類
- Product : 商品
- Order : 使用者的每一筆交易
- OrderDetail : 每筆交易中的每個商品
- Message_Board : 每筆交易中的每個商品
- Administrator : 最高管理員
