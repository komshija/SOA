sleep 15;
/kuiper/bin/kuiperd &
sleep 5;
/kuiper/bin/kuiper create stream co -f /co.txt
/kuiper/bin/kuiper create rule corule -f /corule.txt
/kuiper/bin/kuiper create stream no2 -f /no2.txt
/kuiper/bin/kuiper create rule no2rule -f /no2rule.txt
wait