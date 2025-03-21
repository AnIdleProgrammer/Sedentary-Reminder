using Windows.ApplicationModel.DataTransfer;
using Windows.UI.WebUI;
using static Reminder.GlobalVariable;

namespace Reminder
{
    public class GlobalVariable
    {
        private readonly String APP_NAME = @"Sedentray Reminder";
        private readonly String SOFTWARE_INC_NAME = @"An Idle Programmer Inc.";
        private readonly String REG_START_AT_WINDOWS_START = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
        private int _workingTime = 50;
        private int _restingTime = 10;
        private readonly String _trayIconBase64 = @"AAABAAEA8wAAAAAAIAAaOgAAFgAAAIlQTkcNChoKAAAADUlIRFIAAADzAAABAAgGAAAAZodAPAAAAARnQU1BAACxjwv8YQUAAAABc1JHQgCuzhzpAAAAIGNIUk0AAHomAACAhAAA+gAAAIDoAAB1MAAA6mAAADqYAAAXcJy6UTwAACAASURBVHic7Z15fFTV2cd/z7mzZN/Yd0QWWSIgiwiBEAJo0ba2VaSt1mqtOwpYrW1tLV1cWiUsLq9Ltb611Ve0Ku5CEkMCorLKvhOELCRkn2S2e5/3jxgEDJm5d5Y7Mznfj/MRMufc8zC5vznnnuc5z0OQhIwRV79m69G7xyRSKAeMEURiGJgTmLiLgMgw275Qw8yNACoAeAHeDxL7oVEh2e0lax4dX2+2fbEGmW1AzPEgi5kNJRMZdC0INxFgN9ukSERj/JeYXnAQFWzIm9xitj2xgBRzEJm6qKiflZUXBGGm2bZED3xcePH9j5dnbQaIzbYmmpFiDhK5C0peA/FVRCQ/U50wMwN0rPxY2eBdK+e6zbYnWpE3XoBk316YZLVbPwNhhNm2RDvMWiN5afaaFVM3mG1LNCLMNiCaybmneJzFbjkkhRwciEQyKyjKvatkodm2RCNyZjbIrLvWjWILbw/GtQb1TkD3NDu6pdmxqqRiUTCuGU6+l9VzSV2TB3VNHmw/1BCUazLT3fl5U5YH5WKdBClmA8xYuL4PsbqVBHU1eo0+3eKQlmh76eff6ecYOyT9jmDaZyalFS2zXvzgyJXHq1uuOVLR3IUNbmkxs0fz0hWFK7I+Dq6FsYsUs04uvPajxK7dEz8SwBQj/RPjFFw0JG3NL3807Ce9U0RVsO2LJP718dGnVn9x4rayk05D/ZlR3eLG6PVPZpUF2bSYRIpZJzPvLvkdFPzFSN/0ZKtj5eKLk4JtU6Sz8Ikvq7YfajC0imEVm/KXZY0Ptk2xiNwA08HE+e+nMPEfjPQdMSAZI9CcFmybooGUqsweP7u0n7HOgi+aMX+D3GD0AylmHSSLlKUkyKa3X490O/r1TeyxeHGONxR2RTqLF5PW4rIkXjJSfwQrEREpno+zswstITAtppBi9pPsu7ekQfA1RvrOy+l3870/Gnwi2DZFE7d8r09zZn97ms1CHt2dCb1wkUUutX0gxewnFjTdRkQJevvNy+mL72b1fC4UNkUbc2edX3/DnP6/0hsjR0TCQvy30FgVO0gx+4vAXN1dCEhJsowNhTnRytXT+y3vkmqr1tuPIKZm3VacHgqbYgUpZr9gAtEgvb0mj+qCuTl9t4bComjmyim9jhnpp9jp/GDbEktIMfvBjAXrsghI0dsvd1y3+aGwJ9qZl9tvrM2i/9azAt8PgTkxgxSzHxBxrt4+iiAcPNH8ZijsiQX694jX3YfBhgJ1OgtSzP7ApNvPSQQM7aJ7v6zTkBhvwNNEGBJ8S2IHKWY/YNIG6u0jCOjbT//s01moONmyTm8fIuobCltiBSlmPyBQd719XB6tuX/XxOOhsCcWqKhxy88myEgx+4fM4yWJeKSYJZIYQYpZIokRZPB6lLP9UH2j061i5+EG1Dd7UVrR7HffAT0TkJpgwYiBKbBaCGOHpCeH0FRJiJFijmB2ltY/99nO2vFbD9RjT2mj0jMjLtPpVuF0a3B6VGgasPAJ45mLvjx4ZoqfmYtKWBBgtymItwnE2RRU1Tn3nN83yTl6UAqmjOyCEYNSZXhqhCLFHAFsPVh/xWc7a64r3laNnl3i5tY0ulFZ48Ldy84UqtGMHXrQGGhxqWhxqQA8AHDBntIm7Cltwv8VluE7963jHulxyEix4niV87WszAxMvjDj3+OGZKwKuXGSDpFiDjPljVr3bbsq735nfSVSEy0/qah1DfzVk9+ItqLWZaJ1vvF4GceqWnCsqgUA5r69rgJvr6uYe/1DG9G/R/zJo1XOZ34wpRcmDem5vGdPUWm2vZ0JKeYwULSt+voX3zuMzPPT/nnn3z5HvSP2chQcr3bieLWzC4DfPvHmIfwz/uhvH3p5D7480PjzX1zeH7Mm9HjJbBtjHZkDzA9yFxaXE1FPXZ0Yzp/M6tuwdtvJ7l/PYp2aHul25I7rWv1VpevKSWNTt/7tpYMvEOk/VrpmSZa8Z8+B/GD8wJCYJefEIgipyRacrPcAYOi5DaWYz430M0vCjlfjr4UsCSZSzAHBX78kxpETbbCQYjZEm4AJ8maURApSzIaQApZEHmF1TWU/WGgRTbY+wu3J0BTqdvb75EWdV+FGR0tS6aZnx/sflxgichetHQoNtwHGa0pJgoW+jbLOSAg/HaZZ96zv5vZybwv4ShKYDaJL/O7NvAtExZpK//Eq3sNik1ZeVBSeJPIzFq7vA6j3CCJZWjTCUFXteVg8fyl8PLfUbFsijaCLedLC9fFx8E5UQE8Q0ahgXZdZq2XQvc56em39C1mNwbru6cy656NETU18hATuDMX1g4EiCBal9aUohOR4C/r3SED3dBu6p8VhYM94JMT5XnA1O704UtGC2iY3jlc5UVbtRG2TG6rK8H79UrXI3dxTob6sObXbip7KaQr/6EzZ139ir1XdSlx6qvIt21wWb0uvOO+uxSM9AIXtQwyqmHMXFb8GpssBjifSm+rcPxhwkZeOuDTvd4qfyD4cpKvSjIXFiwniXiLEBeeawaF7mh3TRnfBRUPTMKx/EiyCoAiCoghYlOB/xKrK8GoMVdXg1Rj7vmrC9oMNWPvlSURa8AsDLmj4Z/7S1bcDi7VQjpV9e2GSYrNcL4guY2AKwDYAytevMyF4waQC8GhACQGvFeRlvRxK+1qHDZBxN69KSItPXwmBbCJKDIZR/sDMKkBbiXHrmqVZG41eZ/qCou8rEE+C0DtUX0D+IAQwdnAasi7MwKiBKUiItyA10QK79dv3ilm4PRoamj1wtKjYc7QRn2ytxtYD9fCq5s3gzMwAKlnDHwuWTX0mmNeetWDtFBbiAWaMAnEPAlmNXouZ3QDKmehzFfSHoiVT9gTRVAABiHnE1a/ZevbufS8p/CsCmVbdkJk9AL3q1Tx3FS3LqfO334wFRRcRKcuIkBVK+86FzSqQNSoDk0ZmYEDPBPTuEoc4W+QI11/cHg3HqltwpLwZn+2uwYadtWh2qabYwoxNGtN9hUunFBi9xribVyWkJqb8Hmz5IREPJqKge3xYYw2EHcz8bzVVXVIUpIKChsScu3B9JqC9SIRxwTAiGLDKNWwRtxQ8PuX1jtpNWrg+PpG9fwaJO4nCl9uLAAzrn4SxQ1IxdkgaLjw/FYqIvd1ZTWPsLm3Exr112LK/DruPNkIL6QL4TFhjDaAXyR53z5pHx9f72y9r/tpudgstANNtJBC2Mjis8QkGPX5UbVh2YMWcgI7M6b6bZi1af7XK3ucFCd0VHkINMzSAH/HWeP9c9FLOtw7/5iwomiFI+ScRDBYL1s/gPomYODwdl03sgV5dIupxPCxU1bnw8RcnsGFXDfYfc4RtU40ZtVC1n+Uvn/peh5tQDz4ocutmXQ0FeQT0Cotx7cCaVsms3VCQVvgRFht7/tcl5hmLSh4SwG+MDBROmPkzr+a9rG3ZPWnh+owEaE8QeF6on4sJQNc0G6Zd2BU/ntkXqYmGH7NiDo9Xwyv5X+HjL6pwos4FDoOuNeZ3oVpuKlh+ybfOVmffXtjTEmd5m0ATQ2+JfzDjNQfEbRvyJtfo7evfjX31a0puv16vEOhq3daZhAbtqGhKuJDjXENBWgkp+ouk60ERhAE94rHg6sEYPkCm0vLF4XIHlrx2AAeOO8KygeYhzil6fGoRvo7Fzb1j7VDYxHYihPS+MAIzV4N4Sv6Safv09PMt5gcfFDMbZr3N4MvN3O01gsbsECA7KLSRbvNm9MHcGX2RFKcgyj4iU2FmtLg1vLO+HM+/G9oYEGZWSaOX1yzL+vnMhWt/zaCHQrG5FSyY2aMR3124ZNrT/vbxeefNWFD8pBB0e2CmxR4JdgXzfzgIE4any6V0EGh2erFlfz1W/PcQTja4QzYOM58g0l+hxBQYXmYsy1+a9St/mnco5tyFxb8hooeCY1ls0LtrHG64rD8mZ3aBkbKkko7xqho276vHSx8exb5jJgR3RRjMzMxYUbA0a4GvaLJzinn63UWXKkJ5j6idCJdOyIAe8fhRdh/MHNcNVinikONVNazddhIrPzmOA8cdZptjOqrG9xYunfpYR23aFfOMu9b0ICVuIxE6fdW9tCQrrr+sP75zcY+Y9AtHOprGKNxSjeffO4Lq+tAtv6MB1nBt/tKsf5/r/Xbvzty7iwtIoZzQmRX5WBTCxcPT8ccbhpttiuRrHv3PPqzdVg23N3IPgIQUDV5N0UYXPD5tV3tvf0vMMxcVZwFUHHrLzML3udhuqTY8dvso9O4q6ytHGjUNbix6cjuOV4e+IEAkwsx7M46Vj1y5cu63Yma/9fDHjFfCY5ZZdCzke+cNxku/HSeFHKFkpNjw/H1j8eDPLzDbFFMgomE1vXu/2u57p/9l5oLiuUx4Ndr8ycFgxthu+OV3B6BrqizFHC3UNXnw4geleH9D5yqcwcxuL5SRRXmTD5z+81OibT2AoFWRQNiOMUYC6UlW/ObaoRgzOFUGfEQhzIydhxvwyH/2ozLCS/sEE2bekZ83NfP0n51aZifCmw3ihPCbZRIMTBmVgX/8+iKMHZImhRylEBFGDUrF8/eNxcxx30orF7swRuTeXXJGGq7WO/jBB0Vu/aytJJDZbscYw24VuO7S/rgmp4/ZpkiCzLufVuC5d46YdqY6nLCG2rrmuB6bnh3vAb6emXPrZw7sLELOSLZi6fxMKeQY5YpLeuKpRaPRLS3izk8EHRJIT0pwjm77e9sye6ZJ9oSVkQOT8fID4zG4T5LZpkhCSJ+u8fjX78Zj4vCw5RgwDQshr+3PBAC5i0o+ISDbPJNCiyDg3h8PwYyLukHIZ+NOAzNj4946/P4fuyM602igqEQDCx+fUipm3rw6Fcym5MEKB+nJVjy9aAxmjusuhdzJICJMuCAd/7hvLLqnx67LUajadAAQSLSPIaKYPEzRt1s8ltyRiUG9O5W3TXIWfbrFI++OTAzuE7P3wQ0AIDQWQ8y2JBQM6ZuIv906En27yUguCdA93Y6Hbx6BCwdFXOq6gCHQlInzN6QIgIeabUywyRyUgkdvGYluabG7tJLoJy3Jhr/cNAIThpmWGTo0CFgSLO6xgggjzLYlmLQJOTlBZv+QfJt4u4I/3zQCk0bE1k63QmKEAKin2YYEi6zMLnj89lEyeYCkQxRB+NONw/G9KTFz64OYhwiAY+Ih4urpffCH64fJsEyJXxAR5v/wfNzyvYFmmxIUNGCEAMKXvT9UXD29D27+7kApZIlursrug1/MGWC2GQFDwEABRLdb6rKJ3XHT5dH/y5CYx7zcvrgqu7fZZgQIZQgwN5tthlGmZGbgrh+dDyFzc0kC5JdXDMSs8dF76ooYyQKEkBQuDzXD+iXh1z8eKje7JEFBCMKCqwbjoqGpZptiDIE4AYbumjZmk55kxaO3jkS8PaqfECQRhs0q8KcbhqNXRvQV+GNmTTCw12xD9BBvF3j59+ORGBfSijOSTordpuCF+8ciKT66Jgom1AoQR42Yk+IV/PuBCbKShCSkWBSBfz8wHhnJ0RN4RIw6ITTab7Yh/tDq6B+B5AQ5I0tCT0KcBQ/fPBI2a9RsrlYKVniH2Vb4wzUz+iAzBoPkzaC8aQ80TTv1YmZwOIolRxmDeifixu9Eh9uTwfuVw5++UHPexV/dQoSITb8xbmgaFs0dLM8jB4EtJ1bhw6OPQdW86Bk3/JSYgdaoKBl4cyYjBqagtKIZpZUtZpvSIQy8LABiJn7TbGPORVqSFffMGyzrPAWBrSfeQUn5iwCAzdX/xYbyV+BwOOB0OuHxeKCqqpyh2+Huq85Hj0hPbkB8UAAAk3jZbFvagwh44GfD0E0mpg+YL6s+QHH5C2f+rP5tbKx8HXV1tWhpaZGCPgcpiVY8+PMLICJ439Xlpc0CACzUtI0j8Dd483cHYvT5UerEjxCYGTur16Co7Nl239/r/gBba95GXV0dmpubzxB0BN4SpjGkbxLumxeZeTw01g6uWz71qACA1Y9f6mB8k+UvEuiRYceVWb3MNiPq2V79EQqOP9lhmyMowJf1b6O+vv4MQUvOZPrYbhgzOPImF8HiFeC0ihaq15sXKbNznE3g6YVjYFEieF0TBWytehdFZc/41fYoFWF741vfEnSE3BIRgSIIf7lpOFITI8s9qmnKSuA0MRetmH4cTGvMM+kb7r1miPQnB8iWE6tQXPYPXX2OohjbG789Q0tBf4PdquBPN45ApMwzzFpFvdO6GzijpCtxixU/YrCpe/DDByRj2piuZpoQ9Ww5serUrrVejmIttjdIQXfEiIHJmDg8w2wzAAAMfuCM8jRtrP97ViMB75tjVmug+/0/icxNhmhha9W7hoXcxlFai+0Nq6SgO+DeeUNMj99mhioc7tfb/v6txcKaJVOvYuaq8JrVyuWTesgi5wZhZnxZ9aHupfW5OEpF2FH/Lurrv73LLQGSEyy4erp59cq4dYPrT2uenVXf9rN2v1oGXnJDnSD6bvhMAzJSrHj0lpEyAskgu07m45Oyp4N6zXo6ArfTgzQaCCEEFEU5FSUmf0/AqPNSsG77SdQ2eUwYneoK8rLmnP6Tdh/jC/OmPQfGS+ExqpU7fzBI3iAG+bLqA5/uJ6OUisJ23VZyhm4Nf739ykGmjM3ge8/+2TkX/T2n3phvYcwhIOT5SCePysB1s/tLMRtgW9V7WFv2XEjHqKdSuNxOpPJAKIoiZ+jT6JFuR22TB/u+agrbmAxsK8ibesvZPz/nBvv6v2c1euGZx8zVoTRMEHDzFQNlHi8DbDmxCmvLng/LWEexVrqt2oGI8NOZfWG1hOf+ZeYGjfn69t7r0FtWtCRnD0idzVro3FU/u7Q/+sh6ULo5/dBEuJBuq/bpmmrHnT8I13Jb/KYwb+q2dt/x1TV/yfQt0EQ2hyCLJxFw+SWxU1UgXGyrevdbhybCRavb6h0p6LOYNrorLEpoZ2eN8VJ+3pSnzvW+X3Es+cunfNFioZ7BXnLPm9EXaUnRk5rFbJgZ26s/wtoguZ+McpQ+wY7691DfIAXdRlK8BbdfeV7Irq+x9kVBypQbO2rjd1Da+r9nNTpQ1p/BRwK2DK3x19fN7heMS3Uadp3MxyfH/8dsMwAApaIAO+relTP0acy5uCcS44IfSMIqquodddOxmLSO2uka+diG170ZmT94zqbYewhBFwVi4A+m9saEC9I6/W6ov2yv+hCFQfYjB0o9HYHL5UIq5C430PrYqAjC5n31vhv7CTO+oua44euezfG5Xa77a6R80yueI5+98O7Aice2MjiXiHSXo09JtOCvN42AEinR6hHOtqr3UBRi95NRpNvqG4gIwwck48PPK9HsCvwIKTMXlHobJ218OtvhT3uDaiIuWDblLSL+i5He44ely0oUfrL1xDthcz8ZRbqtvkEIwtQLuwR2EYZXY+2R/GPlsw+smOPyt5vhBf6Iq3fYklNdrxOQrKef1UL4843DZRJ7P9ha9Y5pu9Z6qUcpXC6nXHKj9VTV2+sq4FX1f5kxuJyh3lCQl/0Edq3UdQHD02OvPrXTCNCdCmTCsHR0S5M5vXyxreo9FJdFh5DbOEprsaPhHTQ0NMDpdMLr9Z6R/bOzkBhnQVamgSOSDGgq31OQN91Qgk3D0yODryPo/8b9bgxVqw8VR+o3YnPVW0i0dAEzn5njWtPAgC6BsOIBFO+5G6gKyPu1i9DgLErUejccFxsRV5+BC5RsCCFARLBarZ1udr4yqzdWb9R5+JAAQXQtgFeMjGnoE85+sNBiqbc2kYCuKbZ/93g8f9/YTveL1QszQ1VVuFwuNDY2ora2FjU1NadmPI/Hcyrhnj+idnTdgZbuu875vlI5ALajo04tjy0Wi9+/o7ZltBACFosFCQkJSElJQZcuXZCWloakpCTYbLZTy+7OAjNj4RPbsfOIviKrzNBcTu5a8vTUWr1jGpqZlUbLD/QKGQBmT+jeqX6hgXC6WIUQsNvtSEpKQlxcnK6lKzODExLgKx5XURTY7XbEx8cjLi4OFkvrraFH1EIIWK1W2Gw2EFGnzvBJRJg9obtuMRNB2Ox0DQDdAQWGxEwafmjkaTt3XPQWsw43beKw2WxITEyExWJBcnKyoWfQFk8STvpo0zarpqamIiUl5dRsasRmq9UKu90Oq9UKEcnJpkPM1Au74ok3D8Hj1ff7EsD3ERYxP8gC9SUz9XbLHt0VXWUye78hIlgsFsTHx8NqtUJVVWhahwFA7cLMKK9LBDrwVLaNFRcXh9TUVKSnpyMhIcHw0rgtkYHVatW1ZI81khMsuHxST7xVUq6rHxOysm8vTCp6ynegyOnoFvOMuuLvkSJ0Z9ybPbG73i6dlrYZ7vSZLpAlq63Z1rGYgTNm1MTExFOrAaP2n/4s3RndU21kj+mqW8xESFLslp8C8C9P8tfo/m0Jha7V3YeAIX0iti5dRNJ287ctdY0KmZn9Xi63bWK1vaxW44dg2uzvrCJuY1CvBNgsBLfOpTaBroZOMet+oGHGhXr7ZF3YBelRVLg6Ujh7hjPyMjIrtvU9/c/hGDcWSYiz4LuTDVRmYb5AbxddYs6+vTAJwGC9g1yTY14WQ4nEbAyd2Sf0zl20dqieLrrELGy2O8nA122vLnF6u0gkMUO3VJvuPkREYHGXnj76xCx4nj6TWgNFkuJlHLak8xJnVzB2iJGCc3yZntb6npkZutfLP5nZTz47STo9P7u0v5FuugIz/BZz9u2FSQSk6bm4IgjTRgd4HEwiiQEu6J8Mm1X3fnPy1EVFfqfj8fvqil1cA9LnykqMU6DIFLoSCRQBpOisbEpEZNeUm/1t7/9XBWG+LksAXDAgWebDlkjQ6macNEL/sUgm3OFvW7/EnH19YRyxGKXXkJkyFlsiOYXBswmpk28s8SsBiF9iVlOUZL0HXRVByMqUz8sSSRsjBiYj3qb3uZkpzupM8aelX1dWWO0CsC4xd0m1yTxfEslpCCJ01Zllh4hIJNj8CtTyS20Wm3WW3mCR3jJQRCL5FgN7JujuozFd7k87v8TM0HQ5rwHggv7yYIVEcjaZg/xaMZ8BCfilP99ifvBBQSrpPr88ZrAul7RE0ikYPVh/JBiBMictXO+zuqJPMc9qmt2VFNIdXDpioK4MvBJJp2BQr0RD5V/tGvXw1canmFW3ojsRQfc0O+Ltwa+5I5HEAn266i9hbGGXT7+W72U2e3X7l3pkyPRAEsm56GlAH2xRfD63+hQzWVl3qcZuafqPfEkknYXu6frFTCCfyQr82c0eqXfgnhnSLSWRnAtj5/s1nzr0LWYSY/QO27eb/mcCiaSzYCgGg2mEryZ+PDOrPi9yNr27yplZIjkXvQzogwmBi5lIDNQ7cP9u+qNcooUKx74zaj91xsJoksAwMjMLUJfs7MIOz1B2KOavE/jpIs4mkJwYm2mCyh178cbB30HTNKiqeioxvRSzRA92q4KkeP2uW9t4W4dnKDuemRV9mUUAICUhNlPqljXtxtuH/giNvXC5XHA6nXC5XFLQEkOkJen3+LDH02H4WIdiFopFd+yZkW+cSKfcsRerDv8ZHs0JAGhqakJDQwMcDgdcLpcUs0Q3aUn6Jz0VSoeB3R2uhxmcprfqa3KMzcwVjn148+DvobLn1M9qamrAzKeqJdpsNilmiS5SDDyKkkLGxawQdAdYJ+vMcxTJlDv24vUD93/r57W1tacyjiYmJkohS3SjNx8YAEAj48tsMOsOVUlNig0xlzftaVfIAFBXV4fm5mZ4PB65xJYYIj3ZQGJ8wR0+w8aG8oJMhWMfXj/4m3O+39zcDCKCqqpSyJKwoYETO3q/w5mZNdIdyhXtqXXb3E8dIWdkSaAoIcio1fHMrOifuRPs0TvZlzftwduH/wSNvR22k0KWBEpCnH6dCIgO4z5kxr2vqXDsxduH/wSP1uKzrRSyxAyYtA6XvR1+PZBGAjrdxtFYVqrCsQ//PfgAVB8zskQSyXTsZxaskU4/c7RNWudyP0kk0UbQl9nRtASVQpaYRSh00vEym7lR58SMJmd0LFUrHPs6nZAVAD2EBcOUeGQICxhAuebGYSsAip4v4VigqUXV3YcZjR29H71bzwFQ7tiLNw/+3mwzwspsawoGKXZYz9rU6COsGN+7GZ4ea1HhvADN6G2ShZJACbqYXW4t2JcMKm3up9NjrWMZ8lRhnGM9bJaOz9BaFRX9EnfCjUq0aLcB6DA+QRIgLrf+mdkXPp6ZhUPvBRubI3eZXeHY57f7KSZwl4OO/gU2HV9cNlQjufYJoJN82ZlFgwGdCGgd6rFDMXtBdXoHbGyJTDFXOPbhjYO/C6mQI2rzT20Gjj0BsFt3V6HWwlK+LARGSdpoMqATlTrWY4fLbKvmrYfQtxKPxJk5VLvWzNzuKxKg+hKQt9p4f1cp4CoF4s8LolWSNozMzBSImD1ebrTqfKpucETW8iwUQnb03gyvzQ4HxaO8IQE2lw2KooBAeo9/hwQCMMW1B4GVImBQ+dPAeY9GZyRQhNPgMLDMZja+m+2y2+ut0LehVe/wgpmhswJsSKh07A/JjOzuchhuoNVP4P76FUH0IAvscR2mi/IPbz2gNgEWWTcs2NQ16b9p2Ouu7+j9Dp+ZN+RNbmGG7k2wylqX3i5Bx5/TT7FKVxHEbC9fp0qSBI9ml9eQn/kouztcZvuRNxsH9A761Qlzd4vLm/Zg1aHFncb9dDaJFMTAPhmvHnSOndD/BcmaVnlgxZwOZ0k/Klrwl3oHLqs299v8/dK/wd1Z3E/t4EEQN+GC+cUgAQCUnTRwbwplm88mvq9C2/WOa+bMzMzI7XsnbCJ2E/H7ol4L4myqvzS3xAdfVerXB7O2w1cbP2ZmbbfegavrzX1m7pOQiezud8BCnbNMzjHNAzUILjINBCgyEizYlNcY0scuXw18ipmZT+gd9USd+RtgveNH4eLkmyAQW6l//cENxhEtsC12ZuCpmmZ8VL0TKkd2iG60ccLQ28D/ewAAGYxJREFUBjEd8tXCpxdZhbVG6HRPlVU7TXVPCSFgsVjQJ2EUxrfchM/dTwfV/6t8NeRr1yuBiE79OZLYYlMxqG+9YRdxg6ZhQWUj1Mo/4daBl+GJC38JRT4/B4WKGgN7Sqp20lcTn2Ju8YoTVqs+MTucKiprXabUaW4VF8FisSA+Ph59UkZgfO2t2Kj9T9DGsJYPgaIosFgssFqtEEJAiMi60RmMreI4xvbZo7uvxsBVx+rR5jz5nyMfYk3VVuRP+TP6x3cNrqGdDKdbNeS6Vb2qzxWyTzF/vmJSw4wFa8uEELrOxm072GBa0XUigqIosNvtSE5ORj+MBGpvxSb1WTAFvmQUQsBmsyEhIQHx8fGw2+2tEWAREChzOk3ojq+abeiX4L9DQmXgjooGrGk+0613wFGB0YUL8MiI63DLwEuDbWqnYfeRDoO42oU1uIu6TQ9czAAgSLwL4GY9Buw41IBLJ3TX0yVoENGppXZcXOsXSj+MhFp7A7ap/wuNAvM/n/5FkZaWhsTERFit1ogTMwC40AfVoj8yvKsh0PGMcMit4s6KRnzgaP95u87jwO3bnsH6mj14Yex8uew2wNaDHQZxtQ9p+VjsexbyS8waaR8ICF1iPnC8SU/zoNOeoAdiDLhWxXb1FahkfJNOCAGr1YrExESkpaUhJSUFVqsVihKpRfO6o4knIa5lPRTXHghPBYibAQCakgq29ECjbShuP7gBHzk6fjTTwPjfrz7Bhtp9eGPCrzEqpX84/gExw45DDfo7sfjAn2Z+iVn1qBvJQkw6pp6TDeYHLLcn6PMwDlyrYYf6KlQyZmPbMt5qtSIuLu7UUjvSnpvPJAFInAMNc9rdzkwiwnvd5+D+3S9j2aF34OGOww33NZUhs/BuvHTR3fhJ36mwUKR+kUUWej09zMyscr4/bf07EyXQBIChY8u2ttGDJqcXSQaSfQeTNkFbrd+4qAZhAqhWwZfqv6CRoaNop15tXxYWiyXCxewbIsLfR12PH/S+GFds+AtqPb7D8q/fvAwfVm7GM2NuQ7JFdwGUToVXZVTX6Z9A3ECVP+38uvu6l01vJOjfOfrwM90u6pDQJry2mTQ5ORnnpV+EscqNCCTyse26Z/89Wl9tTM64AOWXvoh+fu5cv3K8GIPX3IYyZ43xD7MTsG7HSXg13TccKw2qX7tmfol55UpSNY3+T68V+ZsiQ8zAmS6rNkH3T8/EeOVWs02LSOyKFYdnPYOHR1znV/sTrnoMWn0rlh58J2ISNEQaBZv9mmDPgj4teinHL8e03+tCgva8XjPKTzqh6v8mChlnP0MnJyejX/pIjBO3gDi6l8ihQCGB+4f8EOunPoLecek+27s0DxbteBGXfvonNHnl0cnT0TTGzsP6N7808LP+tvX7Ds5fNvUTBuuKEG92qWhsjqxjiO0Jun/6KIxRboBgeaigPS7JGIYvpj2G7/e62GdbBmN11VaM+WQhNtTsDYN10YHbq+k+w8xgT+Wx9Ff9ba9vOmLU6mrOwFsl5bqGCAftCXpg+hhkKj+Gor++fKegd3wGXh9/Lx4ecZ1f/uWDjgpklfwWSw6skrHdAAq3VOtfpTLV71o5yu8dM71rS5/HsM7mjaIyeNXI+2W2J+hB6eMxSrkGipyh28UiFNw/5IfYOj3Pr7BOlTXcs/NFfP+zh1Hp1J3oNWbQmPG/Hx3V3Y+hfaWnvS4xk0ZP6jMHcLo11DVF1lK7jdPdVt8IegIylZ+CuFMW+/CLUSn9UTr7OWR1GQ7hh7fyvcqNGFEwH7sbj3XKzbFGhxfV9QZyfpF4Tk97XWJesyxrFWsdZwhsj60HDISwhYlzua0uUn5htmkRT3HWQ1gx2r/AwBpPE0YUzMffDrwZYqsij8OVutPogZk9lclp/9DTR/cWLhN0H8P518e6Vgthpz23Vb+0kRgnpNvKF7cPvAzbc5ahb3wXv9rfv+tfmFh0r18BKbHCvz40dP/v37XY/+dlwICYiWmt3j5l1U4crWzW2y2snP0MnZKSgv6n3FbKt9pG4qEKsxiV0h/bpufh5/1y/Gr/Rd0BjCyYjzVVPtNaRT0n69340kA8NmvaJr19dIvZw15dU38bRv5B4ebcbqufn+G2itwDFeaRYUvGc2PuwPNj7vDrObrcWYs5n/4ZD+z+TxisM489R/UfeQQATZBunekW89pl03ezxrqT/L1VXA4tCjY//HFb2WytFSyiPRY72FiEgl8MmImdM5ZjaJLv4+8eVvHXfSuRXfI7HGvxmUgjKnmzpEx3H2Yu/SRvWpHefsbuRqKP9HYprWw2dvzLBHy5rZKSkmCz2SIyw0gkcEFyX+zNfRLz+mT55ZNee3IXhqy5DcUnd0XFF76/HKloxrYDRo480hoj4xm6EzWV/Y5KOR1jsanmcC631WjrdUhNTUViYuIpQcvn5/Z5Zfw9+MfYO5Gg+A7EcWoeTCv5Hf66byWcamS6MvWSv9nY2QQN6mtG+hm+C2cuLNkLwlA9fWxWgbf/ejEsSvTMZm2VHT0eD5xOJxwOBxwOB1RVPbVR1pZpRM7S7VPurMXIgrtQ6/EvYcWwpD7YMO1RpFmjO83v1Q9+rjvGQmM+WZA31VCiNcN3n8Z4SW8ft0fDe59WGh3SFM5OENiWKigtLe2M5bbk3PSKS8fxS5/H/PPm+NV+b9NxDPz4Zrxe9mmILQsd+ZtPGA2W0q2rNgzfhSS01430e39DhdEhTaNtya0oCuLi4pCUlITk5GTEx8efmpHlUrtj4hU7lmb+Aqsu/q1fy+56bzN+svFx3Lrt6TBYF3zeXW9s0lK93pVGxzQs5vwl0/Yxc4HefofKm7F5X/TF6Z4uaKvVCrvdDrvdDovFIv3OfiJI4Ls9J2DL9CWYmD7EZ3sPq3jmyMcY98k9+KrFeOH4cHPgeBN2HTHiW+btRSumf2Z03MDWh4yHjXR74f3SgIY1i9MF3faSO9r6GZrUG2uz/oo7z5vjl096c/0hDFp9K1aWrY+KE1jPvnMERo7xM+PPABnezg/oLmxUGz9nhu5T6AeOO0yvRxUI50q3I/Efu7BixYW/xBsTf40uNt/F3L2sYu4Xf8eiHS+gwdN+NGEkHOJodnqx01BubNbcGn8SyNgBifnzFXMawPwQ6/wUVY3x8L/3RcSHLzGXK3tdjL25T2JwYk+/2i8/9B4uKvr2sntr/WGsr9FfvSOYMDOWvX4Ibo/+1QMTP1+yYlpAvtuA14cuF56AgWR/Ow83wuHUXz1eEnt0sSVjT+6TmD/ocr/aH3RUYNDqW/Ha8XWnfjZ7/R9x3OSEgl6VUbxd/7M9MzSo7j8EOn7AYi55emotAN0RK6rGWLBiu5ydJQBa840tz7wJ66Y+jFQ//MteVvHjjY9j9qeLMargblS5G7CvSX/oZDD53fO74PHqv5+Z1N0Fy3MDzn4ZnJ0bd8tPmVn36evSymZTC7NLIo/JGRdg2/Q8zOia6bOtBsbqE1uxs7E1i8fWhsOhNu+cVNa6sGW//nP7zKyqqnZFIBtfbQRFzPlPzjpJTDuN9F3+xsGITCskMY8BCd3wwSV/wIPDrtHV78v6I6ExyAcaM55ZZfCLRMPhomU5R4JhR9B8KqpQ72T2UdOkHbYdbMDnu3XlCZR0AmzCgj9eMA+rJy9GD3uaX30ONVfCqYa/LNLOww0o/lL/qS9uLWL+q2DZETQxFy7JXs/A50b6Ln39IJqd+svESGKfmd0uxOFZz2BS+lCfHmmVNRxuDm/hBZdbxdKVB411ZuzIX5r1drBsCW60g4oHmdutS9YhtY0evLw6slMLScwjXrHh02mP4jdDr4LVR4G6/Y7wpnZ+a105jhrY92Fm1giPBNOWoIq5YPnU1QRNd1ohAHi9qAxHKjpPXiiJfv46/Kd4fcJ9HbbZH8Yd7dpGN176QH8KXQBgYGth3tSgplkJehyixsKQv4wZWP7GoWCbI4kxfr3rfzt8/3Bz+E7lLXvjIDyqsU1oAh4Lsjl+lnTVQcHSrOLcRWvfJIgf6O27/VAD3t9QgTmT/IsGknQu5n7xGPY0HUeyJR6ZKf0xNnUQxqYOwqDEnuhqS0Y3Wwp6+lETKxh8trsGn+4wFqTCjM3ezV5DCQg6IiSBxTkLi0crRFuN9E1LsuLVBydAETLmWXImJSd3Y0hSL793t0PJ9Q9tQtlJY8XxVI1yC5dO0X3i0BchOe5TmDd1G6tYZqRvXZMHj8i4bUk7ZHUZbrqQmRkvvF9qWMjM6luhEDIQIjEDALPyMDMMOf2KtlVHRWpeSefjeLUTrxUeN9SXNdY0rzdofuWzCZmYC5ZfUgnCjXpPVAGtm2G/emoHahrCHwAgkZyLxmYvbnhks6Ga49zK4sIVuQad0r4J6an68q/KVoLIsCLveWo7tAgq1i7p3Dzwj12BdFfrWxKCvoN9OiEV866Vc90a0TBmNpTZ7FiV01ApTIkk2LxZXIZdBpIOAAADKrN28aZnx4e0RlPI890UPj6llEGGneP/WXMM63fEZrUDSXSw9UA9nl11xPgFNP6oYGn25qAZdA7Ckryq4ljZzcx8xEhfRmvsdmWNsd1DiSQQ6ps8eOz/9sNr8HGPmSu97nR9x78MEhYx71o5180aL2TNWDa22kYPHv73Pvn8LAk7f3tlPyprjOWrY401Dfz7oqdG+Zf9P0DCllayYNm0t0DoOBavA3YeacTTb5t3+FzS+fj36q/w+R7jx3OZ8E5h3rTngmhSh4Q1R2ypt/FWZj5mtP9bJeV4f0OlDCiRhBRmxvodJ/HSh8Y3X1njRuGIvz6IZvkkrGI+sGKOS9X4qkCusfT1A9guA0okIeRYlROL/7kHAU0ZxHPXPDtefx6hAAh79vZPlk37TNNogdH+zMC9T+/AzsNS0JLgc7jcgZsf22IoiX0bmsZ/z8+bprvscaCYUopBrXU/o2nYYLS/xsD9z+7E/mNh2VeQdBK+OtGCe57cAa/BY40AoDEON6mNfwECm9iNYIqYi17KcQp7y2UMGJ5enW4Nv3tuF0orQ+qHl3QSKmucuP/ZnWhsCSB9FcMrVJ7++Yo5piwbTSuStObRWfUeeKcz2HCu3domD/7wwm5U1UVvqRuJ+dQ3efCHF3fjRK3x+4iZ3RrxnDXLp5oWsmhqxbO1S6ZvYebFzDBc2qKs2onfPb8LTc0yIaBEPy0uFX94YTcOlRlf4TFD0zR+umDJ1NVBNE03EZEBIPeu4jfJQlcGco1uaTa8eP9FsFs7TvgmkbTh8Wq49fGthhLynQ6DC/OXTJ0RJLMMExG1SL3bvFeD8Ukg16iqc+OaP34Bl1vWr5L4xuPV8OM/bQxYyBrzlrqm+EuDZFZARISYi4pyvA1ey/dZw7ZAruNwqpi7+AuZ5VPSIeU1Tsxb/AXqHYYO852CmQ+6vXzppmfHB3ahIBERYgaAz1dMamByXs6MI4Fcp9mp4q7l27ExgDA8Seyy80gD7sjbhoYA91hY42owZgdahjWYRMQz8+nkzM8/nyy2TYIoNZDrKIJw77zByB3XPVimSaKcT3fU4K8v74XLQP3kM2A4NVW7uGD5tC+DY1lwiDgxA0DO3SXDhMA2ItgDuQ4RcMeVg/D9rF7BMk0SpazZdAKPvXrAUMqf02Fm1QtkFeVNNRz0FCoiTsy5C9dnEmlB/cb7UXZv/GLOAFgtEfNUIQkTqsZ4teAY/mmw8kRHMPOqOkftjzc9+72IiFyKKDFn37VulNXC20Nx7YwUG164bywS4hQQRdQ/WxIiWlwqbn18q+G0uP7AzB97N3svLyrKMT3QIWKmqtyF6zMtFs1QFUl/qGlwY+4fv5AnrjoJB8scuPqPn4dUyABARLMtYy1rQjqIn0TEFJW7aN1YQFtDoIxQjyUIuGJyT9x+5SBZNSNG+cf7R7CysCzg52M9MFCUvyRretgGbAfT7+ZZd5eM0QSvJqKu4Rx3+IBk/PbaoeiZERfOYSUhpLbRjYde3oetB8J6jPgbGJ+UHUu7dNfKUaYkfDdVzLPuWTdKYy4mwJSaIymJFtx91fmYdmFYv0ckIeCLPbV47NX9qGk0N36Dwe8LSp67+vExYY9cMk3M2XetG2WxaJsJZDXLBqDVfXXZxB644Tv9kZ5sM9MUiQEam714Jf8Y3ig6HlBCgeDCb3uc3muLnsoJ64F7U8QcCvdTMHjgZ8NwycgM2KQLK+Lxqho27a3DH1/cYzgNbihh1t6pc9TNC6fbKuxizllQPFIRtCPc4/pLzww7/n7rKPTIsEsXVgTCzKht9ODXz+zEkYqIcO+eE2b+OP9Y+RysnBuW0z9hvVtn3LX2QlLEp0RICOe4elEEYdrorvjttUPNNkVyFsveOIgPP6sMKLVPOGGNi/KXTp0ejrHCJubcRevGEvNqELqEa8xASU+24sY5A3DphO5yljaZom3VeGbVYVTVRWFlUMYna/KyckI9TFju0Fb3E1YTISq3jTMHpeDaWf1w0VBzC313RnYeacBLHx7Flv0muZuCBheWJadftmtx6NxWIRdzzoLikUJQiVnup2AydkgqrpnRB+OGppttSsyzu7QR/1nzFTbsip2jrMz8nhDJ14TKbRVSMWfftW6UVeEtIFhCOU64yRyUgusv64/M81IgZBRZUNld2oiVhcdRvD1GK38yv+1xhcZtFbI7cdY960Yxh+bQRKSQnmTF/T8dihEDkhFnl7nHjOLyqDhU1oy//msvKgPIkBktMPMqB5R5G/ImB5az6CxCIuYZ92wYIdi7MxTXjlRuunwAfjCtN6wKyc0yP2BmeFXGx1+cwBP/PRSRvuJQwuCP85OzvoPFFGCmhG8I+l3X6n6i9USUGOxrRzo2C2FgzwTc9aPzMax/stnmRCxHK5uR99oB7D/uCDzrRzQT5F3uoIq51f2krQZR1LifQgEB6NnFjlHnpeDHuf3Qr3u82SaZTnmNEysLj2PL/jocr3ZCFvL8miAKOmhizllYPFoAa8J9+ikaOK9nAi4aloaZF3XD4L5JZpsTNo5UOJC/qRob99biwHGZMfVcMHNBqbdxzoEVcwLaMAiKmHMWFI9UCOsQYBK+zkDXVBsmXJCG8cPSkXleCtJTYudwR22jG7uONGLLgXp8tqsGFTWxv5kVLJj5Pa/LOy+QXe6guIwEYTOIYueuDCHV9W588NkJfPDZCQCt56ovHp6OEQOT0atLXFSdr66ocaL8pBM7Dzdiw+4a7D0qq3IahYguV+IslwAwXOImpvy/0cju0kbsLm0842czx3XD8AHJGDM4FQlxChLjLIg30fXldKtoavGixaVix+FWe/M3nYDbKx98Iwkp5ghkzaYqrNl0Zm51i0IYMSAZowalIHNQCkafnwoigED4+r9T//fHNcbMrQWEubWQMH/9F6dbw87DDdh5pBE7DzdgV2ljZB5qYHDrP17ShhRzO2gaq0JQREWBeFXGl4ca8OVpCQmtFoIggkUhCEFQROuf/XFzM7deU1UZGjM83tb/R6Rwz4JV/tyriOstGj9BArlm2xMpSDGfhaZhu2qxzSa3tx8r/JAgzDTbpnPh8TIAhisiKh2FHo3pU0UTC9eUfbURK+eqkxau/26ipr5HgkJ+IikakGI+DdawHZoyq+ixiysBVAA8e9qv1k2yqHwHEeaaneKoU8LwMmvvQKHlBclT1p4eMbUhb3LLuJufuTQtYeQHcoYOkmsqd2Gxi6J8N5sZe+C2ZuU/eXG7Ef6zF627wKtp8wXhms4eFBMWmOs18Bvw4vGCFdN2+Wqeu6DkYxKYFQ7TQoUGnh1IwXYpZgDM2O+AGO1P4PvkG0uS41K1hQQxlwgjw2FfZ4KZ90DDfxWhPP5x3uQaPX1nLCj5UAhERK1kI0gxBwgz9ufnZRnKD5S7aO1QsPgHwKOJSAZjG4SZHdCw20ve24qW5mwM5FozFpasjuR9jo6QYg4A1vhA/tKpQ4JxrZkL1/6SSfwVzF2ISKb39AEzNDAamHhxQd7UpcG8du6CdQUkOOo2xQIVc6e96TTm3S1WuihY11uTN+25/OQpPZuF6MXMP2Xw8WBdO6ZQUa0x38qq0tu7xdMt2EIGgFK1/jusoiDY1410OuVutqZhe4tTzPz0f6Y0+m6tg8WkfQqcAPAfAP/JXbR2KGvKhYK0a8F0OUTn/LyZ8QER/YuhbspfNm1fqMc7sGKOq+vC9VcksvYuEWaEerxIodMts5mxw+vyzCp6KqcinONm316YJOJtE4WmTQPTZSAeTxRZgSnBgAGVwNuY8SHAxS47fVbyyFRTEnllZxdaLGMsH5FCUSFo+cysA1/up3AyeP779r7W1CzBnE3giQBGE1FPs+3SCzOqAG0rk9hIrK31auqGomU5dWbbdTq5C9d9RMSzzbbDF1LMfsIaH7CqPUZ9uGJoxJ7Ly5q/tptN0HQSYjyBJzK4Owg9w1Hq1hfMqAW4EsAJjXiz0MRG8qBwzZNZZWbb5g+5C4s/IqKIFrQUsx8E4n6KBLKzCy2WCdYJ8GrjARoLonEgTgGTAnA8GAKEOAAKEdn9uSYzOwFoaA3TdhLgAcgDgouZGwVoC7O2lYi+8Gz2biwqyvGG9B8ZBiLdbSXF7ANmPpifN3Ww2XZIIoMZC0o+EQLZZtvRHtI11QHM2NXotQbN/SSJfmxqw6Wx6raKWTGzhu1CUM7nKyY1+G4t6Sx8uGKOyyHEFaxxodm2BJuYFDMzdnjdntmrH59ywmxbJJHHhrzJLfnHy2exhnyzbQkm/w9cJl+V56lcxgAAAABJRU5ErkJggg==";
        private readonly String _configDataName = @"Reminder.config.data";
        private int _autostartup = 0;
        private int _countDownWX = -1;
        private int _countDownWY = -1;
        private static readonly LAN _lan = new();

        public GlobalVariable()
        {
            string locale = Thread.CurrentThread.CurrentCulture.ToString();
            if (locale.Contains(Culture.ZH))
            {
                // read the language string from files!
                LAN.translate(_lan, new LAN(["计时器:", "工作时间:", "休息时间:", "(分钟)", "选项:", "开机自启!", "开始", "停止", "让眼睛休息一下!放松一下!", "注意坐姿", "主界面", "关于", "退出"]));
            }
        }
        public static class Culture
        {
            public static readonly string EN = "en";
            public static readonly string ZH = "zh";
        }

        public LAN Lan
        {
            get { return _lan; }
        }

        public class LAN
        {
            private String timer = "Timer:";
            private String working_time = "WorkingTime:";
            private String resting_time = "RestingTime";
            private String working_time_50 = "50";
            private String resting_time_10 = "10";
            private String minutes = "(Minutes)";
            private String options = "Options:";
            private String auto_startup = "Start application with windows startup";
            private String start = "Start";
            private String stop = "Stop";
            private String resting = "Do something like workout while resting!";
            private String working = "Pay attention to your sitting style!";
            private String preference = "Preference";
            private String about = "About";
            private String exit = "Exit";

            public LAN() { }

            public LAN(String[] texts)
            {
                this.Timer = texts[0];
                this.Working_time = texts[1];
                this.Resting_time = texts[2];
                this.Minutes = texts[3];
                this.Options = texts[4];
                this.Auto_startup = texts[5];
                this.Start = texts[6];
                this.Stop = texts[7];
                this.Resting = texts[8];
                this.Working = texts[9];
                this.Preference = texts[10];
                this.About = texts[11];
                this.Exit = texts[12];
            }

            public static void translate(LAN lan1, LAN lan2)
            {
                lan1.timer = lan2.timer;
                lan1.working_time = lan2.working_time;
                lan1.resting_time = lan2.resting_time;
                lan1.minutes = lan2.minutes;
                lan1.options = lan2.options;
                lan1.auto_startup = lan2.auto_startup;
                lan1.start = lan2.start;
                lan1.stop = lan2.stop;
                lan1.resting = lan2.resting;
                lan1.working = lan2.working;
                lan1.preference = lan2.preference;
                lan1.about = lan2.about;
                lan1.exit = lan2.exit;
            }

            public string Timer { get => timer; set => timer = value; }
            public string Working_time { get => working_time; set => working_time = value; }
            public string Resting_time { get => resting_time; set => resting_time = value; }
            public string Minutes { get => minutes; set => minutes = value; }
            public string Options { get => options; set => options = value; }
            public string Auto_startup { get => auto_startup; set => auto_startup = value; }
            public string Start { get => start; set => start = value; }
            public string Stop { get => stop; set => stop = value; }
            public string Resting { get => resting; set => resting = value; }
            public string Working { get => working; set => working = value; }
            public string Preference { get => preference; set => preference = value; }
            public string About { get => about; set => about = value; }
            public string Exit { get => exit; set => exit = value; }
            public string Working_time_50 { get => working_time_50; set => working_time_50 = value; }
            public string Resting_time_10 { get => resting_time_10; set => resting_time_10 = value; }
        }

        public String RegStartAtWindowStart
        {
            get { return REG_START_AT_WINDOWS_START; }
        }

        public String SoftwareIncName
        {
            get { return SOFTWARE_INC_NAME; }
        }

        public String AppName
        {
            get { return APP_NAME; }
        }

        public int WorkingTime
        {
            get { return _workingTime; }
            set { _workingTime = value; }
        }

        public int RestingTime
        {
            get { return _restingTime; }
            set { _restingTime = value; }
        }

        public String TrayIconBase64
        {
            get { return _trayIconBase64; }
        }

        public String ConfigDataName
        {
            get { return _configDataName; }
        }

        public int Autostartup
        {
            get { return _autostartup; }
            set { _autostartup = value; }
        }

        public int CountDownWX
        {
            get { return _countDownWX; }
            set { _countDownWX = value; }
        }

        public int CountDownWY
        {
            get { return _countDownWY; }
            set { _countDownWY = value; }
        }

    }
}
