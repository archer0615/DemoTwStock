## 執行方法
1. 使用Visual Stuio 2019 (16.8.2)
2. 開啟專案按F5 使用IIS Express運行
3. 預設開啟 [Swagger頁面](https://localhost:44356/swagger)

#### 依照證券代號 搜尋最近n天的資料
``` CSharp
​/api/TWSE/StockNo/{stockNo}/SearchDays/{searchDays}
```
#### 指定特定日期 顯示當天本益比前n名
``` CSharp
/api/TWSE/Date/{date}/Top/{top}
```
#### 指定日期範圍、證券代號 顯示這段時間內殖利率 為嚴格遞增的最長天數並顯示開始、結束日期
``` CSharp
/api/TWSE/StockNo/{stockNo}/Start/{startDate}/End/{endDate}
```

## 其他要求
- 資料必須允許在線新增 (X)
- 必須使用git開發 (O)
- 必須有文件 (O)
- 必須有單元測試 (O)

## 設計
- 單純的WebApi專案
- 搭配最新的.Net 5.0
- 架構分層 WebApi, Extension, Models, Service, Test
- Model層有設計成盡量讓以後功能的response都各別設定一個Model對應到單一的Service Function
- 將查詢過濾邏輯集中控管 擴充方法化 CustomizationQuery
- 建立ServiceBase 將共用方法集中處理(專案變大的話我會個別API對應一個Service)


