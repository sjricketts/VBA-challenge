
Sub Wallstreet():

    ' loops through all worksheets
            ' remember to add in .ws where necessary
            ' ws. for anytime using Cells and Range
    For Each ws In Worksheets
    ' ws = "Sheet1"
    
        ' variable to hold ticker name
        Dim ticker As String
        
        Dim i, last_row As Long
        
        'variable to hold stock volume
        Dim stock_volume As Double
        stock_volume = 0

        ' variables to hold opening and closing values, yearly change, and percent change
        Dim opening_price, closing_price, yr_change, percent_change As Double

        ' headers for the summary table
        ws.Range("I1").Value = "Ticker"
        ws.Range("J1").Value = "Yearly Change"
        ws.Range("K1").Value = "Percent Change"
        ws.Range("L1").Value = "Total Stock Volume"

        ' where to put the information in the summary tables
        Dim summary_row As Integer
        summary_row = 2

        ' find last row in sheet
        last_row = ws.Cells(Rows.Count, 1).End(xlUp).Row
                
        initial_start_price = ws.Cells(2, 3).Value
        
        ' loop through all stocks
        For i = 2 To last_row

            ' if next row is a different stock
            If ws.Cells(i + 1, 1).Value <> ws.Cells(i, 1).Value Then
                ' Set ticker name
                ticker = ws.Cells(i, 1).Value

                ' add to stock volume
                stock_volume = stock_volume + ws.Cells(i, 7).Value

                ' gets opening price for stock
                opening_price = initial_start_price

                ' gets closing price for stock
                closing_price = ws.Cells(i, 6).Value

                ' finds yearly change in stock price
                yr_change = closing_price - opening_price

                ' finds yearly percent change in stock price
                percent_change = (closing_price - opening_price) / opening_price

                ' ticker name to summary table
                ws.Range("I" & summary_row).Value = ticker
                
                ' stock volume amount to summary table
                ws.Range("L" & summary_row).Value = stock_volume

                ' yearly change amount to summary table
                ws.Range("J" & summary_row).Value = yr_change

                ' nested loop conditional formatting for yearly change column
                ' positive change formatted green
                If yr_change >= 0 Then
                    ws.Range("J" & summary_row).Interior.ColorIndex = 4
    
                ' negative change formatted red
                Else
                    ws.Range("J" & summary_row).Interior.ColorIndex = 3
    
                End If

                ' percent yearly change amount to summary table
                ws.Range("K" & summary_row).Value = percent_change
                ws.Range("K" & summary_row).Style = "Percent"
                
                ' nested loop conditional formatting for percent yearly change column
                ' positive change formatted green
                If percent_change >= 0 Then
                    ws.Range("K" & summary_row).Interior.ColorIndex = 4
    
                ' negative change formatted red
                Else
                    ws.Range("K" & summary_row).Interior.ColorIndex = 3
    
                End If


                ' move down one row for the summary table
                summary_row = summary_row + 1
                
                ' reset stock volume total
                stock_volume = 0
                
                ' reset intial stock price
                initial_stock_price = ws.Cells(i + 1, 1).Value

            ' if next row is the same ticker
            Else
            
                ' keeps running total of stock volume for each stock
                stock_volume = stock_volume + ws.Cells(i, 7).Value
                
            End If
        
        Next i
    
        ws.Columns("I:L").AutoFit
    
    Next ws

End Sub
