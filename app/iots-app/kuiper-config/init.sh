sleep 15;
./bin/kuiperd &
sleep 5;
./bin/kuiper create stream co -f /co.txt
./bin/kuiper create rule corule -f /corule.txt
./bin/kuiper create stream no2 -f /no2.txt
./bin/kuiper create rule no2rule -f /no2rule.txt
wait