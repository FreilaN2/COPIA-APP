using System.Security.Policy;
using static System.Net.WebRequestMethods;

namespace SpinningTrainer.View;

public partial class WebView : ContentPage
{
	public WebView()
    {
		InitializeComponent();
        wvPaginaWeb.Source = "https://www.udemy.com/course/curso-net-maui/?utm_source=adwords&utm_medium=udemyads&utm_campaign=LongTail_la.ES_cc.LATAM&campaigntype=Search&portfolio=LATAM&language=ES&product=Course&test=&audience=DSA&topic=&priority=&utm_content=deal4584&utm_term=_._ag_121424001339_._ad_515898216143_._kw__._de_c_._dm__._pl__._ti_aud-2297301418005:dsa-1190286617479_._li_1028538_._pd__._&matchtype=&gad_source=1&gclid=Cj0KCQjw6auyBhDzARIsALIo6v-vgat80KxXpv_I_svoBid5QIl4wjCKqq9GNiTLK1ZTyMP46x8yj_YaAvQuEALw_wcB";
    }
}