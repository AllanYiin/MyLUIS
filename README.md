
# MyLUIS

這是NET Conf. 中課程【打造自己的繁體中文LUIS服務】的課程實作範例
在這範例中將從建置意圖識別模型出發，將模型轉檔後，透過MVC整合前後端，來實現如同微軟認知服務中LUIS的功能。
![File](file.png)


## 使用方法
- 下載本專案
- 請先至[這裡](https://1drv.ms/u/s!AsqOV38qroofiZuLXJdFwSXKpaTPBs4?e=cZKvhq)下載相關資源(意圖清單、詞彙清單、字向量)與模型檔案，主要是因為github有單個檔案大小上限。下載後解壓縮。請將解壓縮後資料夾中的「asset」資料夾複製至專案資料夾的「wwwroot」之下，而「text_cnn.onnx」則複製至專案中的「Models」中，並且將此模型物件的「複製到輸出目錄」的屬性設為「永遠複製」

